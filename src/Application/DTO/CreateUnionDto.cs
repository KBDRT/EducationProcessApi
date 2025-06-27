using EducationProcessAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EducationProcessAPI.Application.DTO
{
    public record CreateUnionDto
    (
        string Description,
        double Duration,
        string Name,
        Guid TeacherId,
        Guid DirectionId
    );
}
