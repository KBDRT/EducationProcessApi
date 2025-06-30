using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
    public record LessonsDateDto
    (
        Guid Id,
        DateOnly? Date
    );
}
