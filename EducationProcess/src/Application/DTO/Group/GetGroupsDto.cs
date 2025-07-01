namespace EducationProcessAPI.Application.DTO
{
    public class GetGroupsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int StartYear { get; set; }

    }
}
