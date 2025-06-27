using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
    public record TeachersForEduYearDto
    (
        string TeacherName,
        List<UnionNameDto>? Unions = null
    );

    public record UnionNameDto
    (
        string UnionName,
        List<GroupsNameDto>? Groups = null
    );

    public record GroupsNameDto
    (
        string GroupName,
        Guid GroupId
    );

}
