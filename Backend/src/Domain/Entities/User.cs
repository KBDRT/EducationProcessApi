namespace EducationProcessAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login {  get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


    }
}
