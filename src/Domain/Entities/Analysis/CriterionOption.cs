using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Domain.Entities.LessonAnalyze
{
    public class CriterionOption
    {
        public Guid Id { get; set; }    

        public string Name { get; set; } = string.Empty;
        public AnalysisCriteria Criterion { get; set; } = new AnalysisCriteria();

    }
}
