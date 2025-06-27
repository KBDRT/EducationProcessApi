using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcess.Presentation.Contracts
{
    public record CreateAnalysisCriteriaDto
    (
        AnalysisTarget AnalysisTarget,
        string Name,
        string Description,
        string WordMark,
        int Order
    );
}
