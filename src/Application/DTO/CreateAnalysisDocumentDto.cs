using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace Application.DTO
{
    public record CreateAnalysisDocumentDto
    (
        Guid LessonId,
        AnalysisTarget Target,
        DateOnly CheckDate,
        string ResultDescription,
        string AuditorName,
        List<Guid>? OptionsId
    );
}
