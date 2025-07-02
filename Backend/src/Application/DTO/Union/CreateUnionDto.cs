namespace EducationProcessAPI.Application.DTO
{
    public record CreateUnionDto
    (
        string Description,
        double Duration,
        string Name,
        Guid TeacherId,
        Guid DirectionId
    );
}
