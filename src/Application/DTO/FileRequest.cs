using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public record FileRequest
    (
        IFormFile? FormFile
    );
}
