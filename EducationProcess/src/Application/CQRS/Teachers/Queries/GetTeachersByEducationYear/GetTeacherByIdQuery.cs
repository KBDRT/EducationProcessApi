using Application.CQRS.Result.CQResult;
using Application.DTO;
using EducationProcessAPI.Application.DTO;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersByEducationYear
{
    public record GetTeachersByEducationYearQuery
    (
        int EducationYear
    )
    : IRequest<CQResult<List<TeachersForEduYearDto>>>;  
}
