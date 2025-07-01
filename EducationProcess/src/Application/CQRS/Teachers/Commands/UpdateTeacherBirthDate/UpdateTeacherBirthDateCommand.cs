using MediatR;
using Application.CQRS.Result.CQResult;

namespace Application.CQRS.Teachers.Commands.UpdateTeacherBirthDate
{
    public record UpdateTeacherBirthDateCommand 
    (
        Guid TeacherId, 
        DateOnly NewbirthDate
    ) 
    : IRequest<CQResult>;
}
