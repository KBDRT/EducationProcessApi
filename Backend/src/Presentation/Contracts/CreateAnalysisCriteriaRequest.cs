using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcess.Presentation.Contracts
{
    public record CreateAnalysisCriteriaRequest
    (
        AnalysisTarget AnalysisTarget,
        string Name,
        string Description,
        string WordMark,
        int Order
    );
}
