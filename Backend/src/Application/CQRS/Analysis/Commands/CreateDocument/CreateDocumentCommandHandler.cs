
using Application.Abstractions.Repositories;
using Application.CQRS.Analysis.Commands.CreateCriteria;
using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
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
        private readonly IBaseRepository<AnalysisCriteria> _baseRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public CreateDocumentCommandHandler(IAnalysisRepository analysisRepository,
                                            ILessonRepository lessonRepository,
                                            IBaseRepository<AnalysisCriteria> baseRepository,
                                            IValidatorFactoryCustom validatorFactory)
        {
            _analysisRepository = analysisRepository;
            _lessonRepository = lessonRepository;
            _baseRepository = baseRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<Guid>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var validation = _validatorFactory.GetValidator<CreateDocumentCommand>().Validate(request);
            var serviceResult = new CQResult<Guid>(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }

            AnalysisDocument document = new(AnalysisTarget.Lesson);
            if (request?.OptionsId?.Count > 0)
            {
                var criteriasCount = _baseRepository.GetRecordsCount();
                var criterias = await _analysisRepository.GetCriteriasByOptionsIdAsync(request.OptionsId, cancellationToken);

                if (criteriasCount != criterias.Count)
                {
                    serviceResult.AddMessage("Не все критерии заполнены", "OptionsId");
                    return serviceResult;
                }

                document.AddCriterias(criterias);
            }
            
            document.SetChildrenCount(request.ChildrenCount);
            document.SetDesctiption(request.ResultDescription);
            document.SetDate(request.CheckDate);
            document.SetAuditor(request.AuditorName);

            var lesson = await _lessonRepository.GetWithIncludesByIdAsync(request.LessonId);
            document.SetLesson(lesson);

            var documentStatus = document.IsDocumentCorrect();
            if (documentStatus.IsValid)
            {
                var id = await _analysisRepository.CreateDocumentAsync(document, cancellationToken);
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
