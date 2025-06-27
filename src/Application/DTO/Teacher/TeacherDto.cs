using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
    public record TeacherDto
    (
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    );
}
