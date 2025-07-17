using Domain.Entities;
using Domain.Entities.Analysis;

namespace EducationProcessAPI.Domain.Entities.LessonAnalyze
{
    public class CriterionOption : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public AnalysisCriteria Criterion { get; set; } = new AnalysisCriteria();

        public List<AnalysisDocument> Document { get; set; } = [];

    }
}
