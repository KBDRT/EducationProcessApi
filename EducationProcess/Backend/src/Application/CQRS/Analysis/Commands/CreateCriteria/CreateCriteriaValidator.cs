using FluentValidation;

namespace Application.CQRS.Analysis.Commands.CreateCriteria
{
    public class CreateCriteriaValidator : AbstractValidator<CreateCriteriaCommand>
    {

        public CreateCriteriaValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.AnalysisTarget).NotEmpty();
            RuleFor(x => x.Order).NotEmpty();
            RuleFor(x => x.WordMark).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }

    }
}
