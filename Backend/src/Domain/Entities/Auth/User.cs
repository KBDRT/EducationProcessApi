namespace Domain.Entities.Auth
{
    public class User : BaseEntity
    {
        public string Login {  get; set; } = string.Empty;

        public string PasswordHashed { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public PersonInitials Initials { get; set; } = new();

        public List<Role> Roles { get; set; } = [];

    }
}
