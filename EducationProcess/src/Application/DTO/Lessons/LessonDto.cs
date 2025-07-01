namespace EducationProcessAPI.Application.DTO
{
   public record LessonDto
   (
     Guid GroupId,
     string Name,
     DateTime? Date,
     double StudyHours,
     string FormExercise,
     string Place,
     string FormControl
   );
}
