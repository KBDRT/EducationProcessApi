using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.DeleteTeacherById
{
    public record DeleteTeacherByIdCommand
    (
        Guid TeacherId
    )
    : IRequest<CQResult>;
}
