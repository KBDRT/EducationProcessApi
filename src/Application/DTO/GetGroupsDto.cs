using EducationProcessAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Application.DTO
{
    public class GetGroupsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int StartYear { get; set; }

    }
}
