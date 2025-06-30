using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public record CreateGroupFromFileDto
    (
        Guid UnionId, 
        IFormFile File, 
        int StartYear
    );
}
