using Domain.Entities.Analysis;
using FluentValidation;

namespace Domain.Validators
{
    public class AnalysisDocumentValidator : AbstractValidator<AnalysisDocument>
    {
        public AnalysisDocumentValidator()
        {
            RuleFor(x => x.Teacher).NotNull();
            RuleFor(x => x.ArtUnion).NotNull();
            RuleFor(x => x.Lesson).NotNull();
            RuleFor(x => x.ChildrenCount).GreaterThan(0);

            RuleFor(x => x.SelectedOptions.Count).GreaterThan(0);
        }
    }
}
