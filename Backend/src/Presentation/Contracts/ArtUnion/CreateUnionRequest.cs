using EducationProcessAPI.Domain.Entities;

namespace EducationProcess.Presentation.Contracts.ArtUnion
{
    public record CreateUnionRequest
    (
        Guid TeacherId,
        Guid DirectionId,
        string Name,
        double Duration,
        string Description
    );
}
