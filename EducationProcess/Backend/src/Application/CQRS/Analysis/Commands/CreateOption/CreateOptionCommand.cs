using Application.CQRS.Result.CQResult;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateOption
{
    public record CreateOptionCommand
    (
        Guid CriteriaId,
        string OptionName
    )
    : IRequest<CQResult<Guid>>;
}
