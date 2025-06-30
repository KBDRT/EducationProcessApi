using FluentValidation;
using Application.DTO;


namespace Application.Validators.CRUD.Create
{
    public class CreateOptionValidator : AbstractValidator<CreateOptionDto>
    {
        public CreateOptionValidator()
        {
            RuleFor(x => x.CriteriaId).NotEqual(Guid.Empty);
            RuleFor(x => x.OptionName).NotEmpty();
        }
       

    }
}
