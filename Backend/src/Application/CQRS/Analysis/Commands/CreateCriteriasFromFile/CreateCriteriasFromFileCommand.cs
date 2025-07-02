
using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Analysis.Commands.CreateCriteriasFromFile
{
    public record CreateCriteriasFromFileCommand
    (
        AnalysisTarget Target,
        IFormFile File,
        bool IsDeletePrev
    )
    : IRequest<CQResult>;
}
