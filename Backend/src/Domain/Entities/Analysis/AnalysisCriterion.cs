using Domain.Entities;
using Domain.Entities.Analysis;

namespace EducationProcessAPI.Domain.Entities.LessonAnalyze
{
    public enum AnalysisTarget
    {
        Lesson,
        Event
    }

    public class AnalysisCriteria : BaseEntity
    {
        public AnalysisTarget AnalysisTarget { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string WordMark { get; set; } = string.Empty;
        public int Order { get; set; }

        public List<CriterionOption> Options { get; set; } = [];

    }
}
