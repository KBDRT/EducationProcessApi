using Application.Abstractions.Repositories;
using Application.Abstractions.S3;
using Application.CQRS.Result.CQResult;
using Domain.Entities.Analysis;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Fillers;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateFileForDocument
{
    public class CreateFileForDocumentCommandHandler : IRequestHandler<CreateFileForDocument, CQResult<Guid>>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IMinioHelper _minioHelper;
        private readonly IFillFile<AnalysisDocument> _wordFiller;
        private const string _TEMPLATE_NAME = "Шаблон.docx";

        public CreateFileForDocumentCommandHandler(IAnalysisRepository analysisRepository,
                                                   IMinioHelper minioHelper,
                                                   IFillFile<AnalysisDocument> wordFiller)
        {
            _analysisRepository = analysisRepository;
            _minioHelper = minioHelper;
            _wordFiller = wordFiller;
        }

        public async Task<CQResult<Guid>> Handle(CreateFileForDocument request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<Guid>();

            var documentInfo = await _analysisRepository.GetDocumentByIdAsync(request.DocumentId);
            if (documentInfo == null)
            {
                return serviceResult;
            }

            using var templateStream = await _minioHelper.GetFileToStreamAsync(_TEMPLATE_NAME);
            using var documentStream = await _wordFiller.FillAsync(templateStream, documentInfo);

            if (documentStream.Length > 0)
            {
                Guid fileId = Guid.NewGuid();
                await _minioHelper.PutFileFromStreamAsync(documentStream, $"{fileId.ToString()}.docx");
                await _analysisRepository.SetFileForDocumentAsync(request.DocumentId, fileId, cancellationToken);
                serviceResult.SetResultData(fileId);
            }

            return serviceResult;
        }
    }
}
