using Application.CQRS.Helpers.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.UpdateTeacherBirthDate
{
    public record UpdateTeacherBirthDateCommand 
    (
        Guid TeacherId, 
        DateOnly NewbirthDate
    ) 
    : IRequest<CQResult>;
}
