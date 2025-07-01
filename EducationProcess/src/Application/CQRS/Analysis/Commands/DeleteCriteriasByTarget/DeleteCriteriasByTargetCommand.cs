using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.DeleteCriteriasByTarget
{
    public record DeleteCriteriasByTargetCommand
    (
        AnalysisTarget Target
    )
    :IRequest<CQResult>;

}
