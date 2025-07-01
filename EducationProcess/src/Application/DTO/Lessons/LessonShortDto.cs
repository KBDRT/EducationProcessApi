namespace EducationProcessAPI.Application.DTO
{
   public record LessonShortDto
   (
     Guid Id,
     string Name,
     DateTime? Date,
     double StudyHours,
     string FormExercise,
     string Place,
     string FormControl
   );
}
