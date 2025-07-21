using Application.CQRS.Result.CQResult;
using Domain.Entities.Analysis;
using EducationProcessAPI.Application.Abstractions.Repositories;
using MediatR;

namespace Application.CQRS.Analysis.Querires.GetDocuments
{
    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, CQResult<List<AnalysisDocument>>>
    {
        private readonly IAnalysisRepository _analysisRepository;

        public GetDocumentsQueryHandler(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<CQResult<List<AnalysisDocument>>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<List<AnalysisDocument>>();
            var documents = await _analysisRepository.GetDocumentsAsync(request.page, request.size);

            serviceResult.SetResultData(documents);

            return serviceResult;
        }

    }
}
