using Application.CQRS.Helpers.CQResult;
using Application.DTO;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeachersPaginationAfter
{
    public record GetTeachersAfterIdQuery
    (
        Guid AfterTeacherId, 
        int ListSize
    )
    : IRequest<CQResult<List<TeacherDto>>>;  
}
