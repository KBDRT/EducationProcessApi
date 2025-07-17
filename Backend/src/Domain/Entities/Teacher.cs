using Domain.Entities;
using Domain.Entities.Auth;

namespace EducationProcessAPI.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public PersonInitials Initials { get; set; } = new();

        public DateOnly BirthDate { get; set; } = new DateOnly();

        public List<ArtUnion> Union { get; set; } = new List<ArtUnion>();

        public User? User { get; set; }

    }
}
