using EducationProcessAPI.Application.DTO;
using FluentValidation;

namespace Application.Validators.CRUD.Create
{
    public class CreateLessonValidator : AbstractValidator<LessonDto>
    {
        public CreateLessonValidator()
        {
            RuleFor(x => x.GroupId).NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.StudyHours).GreaterThan(0);
            RuleFor(x => x.FormExercise).NotEmpty();
            RuleFor(x => x.Place).NotEmpty();
            RuleFor(x => x.FormControl).NotEmpty();
        }

    }
}
