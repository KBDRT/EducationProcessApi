using Domain.Entities;
using Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationProcessAPI.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }

        public PersonInitials Initials { get; set; } = new();

        public DateOnly BirthDate { get; set; } = new DateOnly();

        public List<ArtUnion> Union { get; set; } = new List<ArtUnion>();

        public User? User { get; set; }

    }
}
