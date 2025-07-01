using FluentValidation;
using Application.DTO;


namespace Application.CQRS.Analysis.Commands.CreateOption
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionValidator()
        {
            RuleFor(x => x.CriteriaId).NotEqual(Guid.Empty);
            RuleFor(x => x.OptionName).NotEmpty();
        }
       

    }
}
