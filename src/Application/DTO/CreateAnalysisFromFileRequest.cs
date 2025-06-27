using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Microsoft.AspNetCore.Http;

namespace Application.DTO
{
    public record CreateAnalysisFromFileRequest
    (
        AnalysisTarget Target,
        IFormFile File,
        bool IsDeletePrev
    );
}
