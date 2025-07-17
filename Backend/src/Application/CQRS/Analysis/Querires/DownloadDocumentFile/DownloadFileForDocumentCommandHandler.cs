using Application.Abstractions.Repositories;
using Application.Abstractions.S3;
using Application.CQRS.Analysis.Commands.CreateFileForDocument;
using Application.CQRS.Result.CQResult;
using Domain.Entities.Analysis;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Fillers;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Threading;

namespace Application.CQRS.Analysis.Querires.DownloadFileForDocument
{
    public class DownloadFileForDocument : IRequestHandler<DowndloadFileForDocumentCommand, CQResult<MemoryStream>>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IMinioHelper _minioHelper;
        private readonly IMediator _mediator;

        public DownloadFileForDocument(IAnalysisRepository analysisRepository,
                                       IMinioHelper minioHelper,
                                       IMediator mediator)
        {
            _analysisRepository = analysisRepository;
            _minioHelper = minioHelper;
            _mediator = mediator;
        }

        public async Task<CQResult<MemoryStream>> Handle(DowndloadFileForDocumentCommand request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<MemoryStream>();

            var fileId = await GetFileId(request.DocumentId, cancellationToken);
            var fileFromMinio = new MemoryStream();
            if (fileId != Guid.Empty)
            {
                fileFromMinio = await GetFileFromMinio(fileId);
            }

            if (fileFromMinio.Length == 0)
            {
                await _mediator.Send(new CreateFileForDocument(request.DocumentId));
                fileId = await GetFileId(request.DocumentId, cancellationToken);
                fileFromMinio = await GetFileFromMinio(fileId);
                serviceResult.SetResultData(fileFromMinio);
            }
            else
            {
                serviceResult.SetResultData(fileFromMinio);
            }

            return serviceResult;
        }

        private async Task<Guid> GetFileId(Guid documentId, CancellationToken cancellationToken)
        {
            return await _analysisRepository.GetFileIdForDocumentAsync(documentId, cancellationToken);
        }

        private async Task<MemoryStream> GetFileFromMinio(Guid fileId)
        {
            return await _minioHelper.GetFileToStreamAsync($"{fileId.ToString()}.docx");
        }

    }
}
