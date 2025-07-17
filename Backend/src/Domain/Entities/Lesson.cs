using Domain.Entities;

namespace EducationProcessAPI.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public DateTime? Date { get; set; }

        public double StudyHours { get; set; }

        public string FormExercise { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public string FormControl { get; set; } = string.Empty;

        public Group Group { get; set; } = new Group();

    }
}
