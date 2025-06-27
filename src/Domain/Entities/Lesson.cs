using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcessAPI.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? Date { get; set; }

        public double StudyHours { get; set; }

        public string FormExercise { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public string FormControl { get; set; } = string.Empty;

        public Group Group { get; set; } = new Group();





    }
}
