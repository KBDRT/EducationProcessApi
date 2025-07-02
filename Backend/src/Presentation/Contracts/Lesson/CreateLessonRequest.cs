namespace EducationProcess.Presentation.Contracts.Lesson
{
    public record CreateLessonRequest
    (
        Guid groupid,
        string Name,
        DateTime? Date,
        double StudyHours,
        string FormExercise,
        string Place,
        string FormControl
    );
}
