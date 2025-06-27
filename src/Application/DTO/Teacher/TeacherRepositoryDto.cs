namespace EducationProcessAPI.Application.DTO
{
    public record UpdateBirthDateDto(Guid id, DateOnly birthDate);

    public record GetAfterIdWithPaginationDto(Guid afterId, int size);

}
