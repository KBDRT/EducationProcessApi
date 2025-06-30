using Application.CQRS.Helpers.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.CreateTeacher
{
    public record CreateTeacherCommand 
    (
        string Surname,
        string Name,
        string Patronymic,
        DateOnly BirthDate
    ) 
    : IRequest<CQResult<Guid>>;
}
