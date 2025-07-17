
namespace Domain.Entities.Auth
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string NameRu { get; set; } = string.Empty;  

        public string Description { get; set; } = string.Empty;

        public List<Permission> Permissions { get; set; } = [];

        public List<User> Users { get; set; } = [];

    }
}
