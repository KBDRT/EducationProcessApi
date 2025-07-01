using EducationProcessAPI.Domain.Entities;

namespace EducationProcessAPI.Application.DTO
{
    public class GetUnionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DirectionName { get; set; } = string.Empty;

        public double EduDuration { get; set; }

        public string Description { get; set; } = string.Empty;

        public List<GetGroupsDto> Groups { get; set; } = new List<GetGroupsDto>();

        public static List<GetGroupsDto> ListCreateGroup(List<Group> groups)
        {
            List<GetGroupsDto> shortFormatGroup = new List<GetGroupsDto>();

            foreach (var group in groups)
            {
                GetGroupsDto getGroupsDto = new GetGroupsDto()
                {
                    Id = group.Id,
                    Name = group.Name,
                    StartYear = group.StartYear,
                };

                shortFormatGroup.Add(getGroupsDto);
            }

            return shortFormatGroup;
        }

    };
}
