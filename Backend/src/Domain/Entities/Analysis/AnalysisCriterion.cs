using Domain.Entities.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Domain.Entities.LessonAnalyze
{
    public enum AnalysisTarget
    {
        Lesson,
        Event
    }


    public class AnalysisCriteria
    {
        public Guid Id { get; set; }

        public AnalysisTarget AnalysisTarget { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string WordMark { get; set; } = string.Empty;
        public int Order { get; set; }

        public List<CriterionOption> Options { get; set; } = [];

        public List<AnalysisDocument> Document { get; set; } = [];

    }
}
