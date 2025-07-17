using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Querires.DownloadFileForDocument
{
    public record DowndloadFileForDocumentCommand
    (
        Guid DocumentId
    )
    : IRequest<CQResult<MemoryStream>>;
}
