using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.UpdateTeacher
{
    public record UpdateTeacherCommand 
    (
        Guid Id,
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    ) 
    : IRequest<CQResult>;
}
