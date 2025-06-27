using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
    public record GetCriteriasWithOptionsDto
    (
        string CriteriaName,
        string CriteriaDescription,
        List<GetOptionsDto> Options
    );

   public record GetOptionsDto
   (
       string OptionName
   );
}
