
using Application.CQRS.Result.CQResult;
using Domain.Entities.Analysis;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, CQResult<Guid>>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly ILessonRepository _lessonRepository;

        public CreateDocumentCommandHandler(IAnalysisRepository analysisRepository,
                                            ILessonRepository lessonRepository)
        {
            _analysisRepository = analysisRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<CQResult<Guid>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<Guid>();
            AnalysisDocument document = new AnalysisDocument(AnalysisTarget.Lesson);

            document.SetDesctiption(request.ResultDescription);
            document.SetDate(request.CheckDate);
            document.SetAuditor(request.AuditorName);
            document.SetLesson(await _lessonRepository.GetWithIncludesByIdAsync(request.LessonId));

            var documentStatus = document.IsDocumentCorrect();

            if (documentStatus.IsValid)
            {
                var id = await _analysisRepository.CreateDocumentAsync(document);
                serviceResult.SetResultData(id);

                return serviceResult;
            }
            else
            {
                serviceResult.AddMessages(documentStatus);
                return serviceResult;
            }
        }
    }
}
