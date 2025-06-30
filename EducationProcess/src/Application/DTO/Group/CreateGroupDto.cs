namespace Application.DTO
{
    public record CreateGroupDto
    (
        string GroupName, 
        int StartYear, 
        Guid UnionId
    );
}
