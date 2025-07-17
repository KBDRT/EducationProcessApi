
using Domain.Entities;

namespace EducationProcessAPI.Domain.Entities
{
    public class ArtDirection : BaseEntity
    {
        public string ShortName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }
}
