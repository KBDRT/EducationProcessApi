using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [MaxLength(4)]
        public int StartYear { get; set; }   

        public ArtUnion ArtUnion { get; set; } = new ArtUnion();

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
