namespace EducationProcessAPI.Application.DTO
{
    public record TeachersForEduYearDto
    (
        string TeacherName,
        List<UnionNameDto>? Unions = null
    );

    public record UnionNameDto
    (
        string UnionName,
        List<GroupsNameDto>? Groups = null
    );

    public record GroupsNameDto
    (
        string GroupName,
        Guid GroupId
    );

}
