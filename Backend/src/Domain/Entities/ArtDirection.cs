
namespace EducationProcessAPI.Domain.Entities
{
    public class ArtDirection
    {
        public Guid Id { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

    }
}
