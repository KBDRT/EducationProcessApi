
namespace EducationProcessAPI.Application.DTO
{
    public record GetCriteriasWithOptionsDto
    (
        string CriteriaName,
        string CriteriaDescription,
        Guid CriteriaId,
        List<GetOptionsDto> Options
    );

   public record GetOptionsDto
   (
       string OptionName,
       Guid OptionId
   );
}
