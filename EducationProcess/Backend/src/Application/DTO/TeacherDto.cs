namespace Application.DTO
{
    public record TeacherDto
    (
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    );
}
