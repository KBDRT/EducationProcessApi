namespace EducationProcess.Presentation.Contracts.Teachers
{
    public record BaseTeacherRequest
    (
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    );
}
