using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using MediatR;

namespace Application.CQRS.Analysis.Commands.CreateFileForDocument
{
    public record CreateFileForDocument
    (
        Guid DocumentId
    )
    : IRequest<CQResult<Guid>>;
}
