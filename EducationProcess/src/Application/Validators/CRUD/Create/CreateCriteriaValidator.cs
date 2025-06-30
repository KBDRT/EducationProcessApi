using DocumentFormat.OpenXml.Office2010.Excel;
using EducationProcess.Presentation.Contracts;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateCriteriaValidator : AbstractValidator<CreateAnalysisCriteriaDto>
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
