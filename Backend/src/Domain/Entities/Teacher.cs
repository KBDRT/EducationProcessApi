using System.ComponentModel.DataAnnotations.Schema;

namespace EducationProcessAPI.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; } = new DateOnly();
        public List<ArtUnion> Union { get; set; } = new List<ArtUnion>();

        [NotMapped]
        public string Initials
        {
            get
            {
                string output = "";

                if (!String.IsNullOrEmpty(Surname))
                {
                    output = Surname;
                }

                if (!String.IsNullOrEmpty(Name))
                {
                    output += $" {Name[0]}.";
                }

                if (!String.IsNullOrEmpty(Patronymic))
                {
                    output += $" {Patronymic[0]}.";
                }

                return output;
            }
        }


    }
}
