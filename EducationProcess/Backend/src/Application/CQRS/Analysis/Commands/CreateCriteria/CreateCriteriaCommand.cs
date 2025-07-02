
using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateCriteria
{
    public record CreateCriteriaCommand
    (
        AnalysisTarget AnalysisTarget,
        string Name,
        string Description,
        string WordMark,
        int Order
    )
    : IRequest<CQResult<Guid>>;
}
