using Application.CQRS.Helpers.CQResult;
using Application.DTO;
using MediatR;

namespace Application.CQRS.Teachers.Queries.GetTeacherById
{
    public record GetTeacherByIdQuery
    (
        Guid TeacherId
    )
    : IRequest<CQResult<TeacherDto>>;  
}
