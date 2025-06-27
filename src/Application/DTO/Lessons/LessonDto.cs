using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
   public record LessonDto
   (
     Guid groupId,
     string Name,
     DateTime? Date,
     double StudyHours,
     string FormExercise,
     string Place,
     string FormControl
   );
}
