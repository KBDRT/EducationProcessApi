using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Teachers.Commands.DeleteAllTeachers
{
    public record DeleteAllTeachersCommand 
    (
      
    ) 
    : IRequest<CQResult<int>>;
}
