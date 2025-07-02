using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateDocument
{
    public record CreateDocumentCommand
    (
        Guid LessonId,
        AnalysisTarget Target,
        DateOnly CheckDate,
        string ResultDescription,
        string AuditorName,
        List<Guid>? OptionsId
    )
    : IRequest<CQResult<Guid>>;
}
