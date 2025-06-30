using Application.CQRS.Helpers.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.DeleteAllTeachers
{
    public record DeleteAllTeachersCommand 
    (
      
    ) 
    : IRequest<CQResult<int>>;
}
