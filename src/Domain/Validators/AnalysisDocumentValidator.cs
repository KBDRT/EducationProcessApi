using Domain.Entities.Analysis;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
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

            RuleFor(x => x.SelectedCriterias.Count).GreaterThan(0);
        }
    }
}
