
using Domain.Entities;

namespace EducationProcessAPI.Domain.Entities
{
    public class ArtUnion : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ArtDirection Direction { get; set; } = new ArtDirection();

        public double EduDuration { get; set; }

        public string Description { get; set; } = string.Empty;

        public Teacher Teacher { get; set; } = new Teacher();   

        public List<Group> Groups { get; set; } = new List<Group>();    


    }
}
