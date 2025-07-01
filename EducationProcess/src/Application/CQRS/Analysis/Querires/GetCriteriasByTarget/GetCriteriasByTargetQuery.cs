using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Querires.GetCriteriasByTarget
{
    public record GetCriteriasByTargetQuery
    (
        AnalysisTarget Target
    )
    : IRequest<CQResult<List<GetCriteriasWithOptionsDto>>>;
}
