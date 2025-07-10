using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Owned]
    public class PersonInitials
    {
        public string Surname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;

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
