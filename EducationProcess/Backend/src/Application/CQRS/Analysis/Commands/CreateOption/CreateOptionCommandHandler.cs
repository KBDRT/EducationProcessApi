using Application.CQRS.Result.CQResult;
using Application.Validators.Base;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.Parsers;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateOption
{
    public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, CQResult<Guid>>
    {
        private readonly IAnalysisRepository _analysisRepository;
        private readonly IValidatorFactoryCustom _validatorFactory;

        public CreateOptionCommandHandler(IAnalysisRepository analysisRepository,
                                          IValidatorFactoryCustom validatorFactory)
        {
            _analysisRepository = analysisRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<CQResult<Guid>> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var validation = _validatorFactory.GetValidator<CreateOptionCommand>().Validate(request);
            var serviceResult = new CQResult<Guid>(validation);

            if (!validation.IsValid)
            {
                return serviceResult;
            }

            var criteria = await _analysisRepository.GetCriteriaByIdAsync(request.CriteriaId);

            if (criteria == null)
            {
                serviceResult.AddMessage("Criteria не найден", "criteriaId");
                return serviceResult;
            }

            CriterionOption option = new CriterionOption()
            {
                Id = Guid.NewGuid(),
                Name = request.OptionName,
                Criterion = criteria
            };

            var id = await _analysisRepository.CreateOptionAsync(option);
            serviceResult.SetResultData(id);

            return serviceResult;
        }
    }
}
