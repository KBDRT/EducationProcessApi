using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using DocumentFormat.OpenXml.Office2016.Excel;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateCriteria
{
    public class CreateCriteriaCommandHandler : IRequestHandler<CreateCriteriaCommand, CQResult<Guid>>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public CreateCriteriaCommandHandler(IAnalysisRepository analysisRepository,
                                            IValidatorFactoryCustom validatorFactory)
        {
            _analysisRepository = analysisRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<Guid>> Handle(CreateCriteriaCommand request, CancellationToken cancellationToken)
        {
            var validation = _validatorFactory.GetValidator<CreateCriteriaCommand>().Validate(request);
            var serviceResult = new CQResult<Guid>(validation);

            if (validation.IsValid)
            {
                AnalysisCriteria criteria = CreateNewCriteria(request);
                var id = await _analysisRepository.CreateCriteriaAsync(criteria, cancellationToken);
                serviceResult.SetResultData(id);
            }

            return serviceResult;
        }

        private AnalysisCriteria CreateNewCriteria(CreateCriteriaCommand request)
        {
            return new AnalysisCriteria()
            {
                Id = Guid.NewGuid(),
                AnalysisTarget = request.AnalysisTarget,
                Description = request.Description ?? string.Empty,
                Name = request.Name,
                Order = request.Order,
                WordMark = request.WordMark,
            };
        }
    }
}
