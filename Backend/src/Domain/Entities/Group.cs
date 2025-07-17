using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace EducationProcessAPI.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [MaxLength(4)]
        public int StartYear { get; set; }   

        public ArtUnion ArtUnion { get; set; } = new ArtUnion();

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}
