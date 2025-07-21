using Application.CQRS.Result.CQResult;
using Domain.Entities.Analysis;
using MediatR;

namespace Application.CQRS.Analysis.Querires.GetDocuments
{
    public record GetDocumentsQuery
    (
        int page,
        int size
    )
    : IRequest<CQResult<List<AnalysisDocument>>>;
}
