using System.ComponentModel.DataAnnotations;

namespace EducationProcess.Presentation.Contracts.Group
{
    public record CreateGroupRequest
    (
        Guid UnionId,
        string Name,
        int StartYear
    );
}
