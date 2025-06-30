namespace EducationProcessAPI.Application.DTO
{
    public record CreateTeacherDto
    (
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    );
}
