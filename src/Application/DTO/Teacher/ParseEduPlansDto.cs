using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.DTO
{
    public record ParseEduPlansDto
    (
        List<Group> groups,
        List<Lesson> lessons
    );
}
