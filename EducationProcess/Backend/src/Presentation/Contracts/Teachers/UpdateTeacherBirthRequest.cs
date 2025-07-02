namespace EducationProcess.Presentation.Contracts.Teachers
{
    public record UpdateTeacherBirthRequest
    (
        Guid Id,
        DateOnly BirthDate
    );
}
