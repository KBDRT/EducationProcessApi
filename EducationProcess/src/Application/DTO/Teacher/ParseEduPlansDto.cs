using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.DTO
{
    public record ParseEduPlansDto
    (
        List<Group> Groups,
        List<Lesson> Lessons
    );
}
