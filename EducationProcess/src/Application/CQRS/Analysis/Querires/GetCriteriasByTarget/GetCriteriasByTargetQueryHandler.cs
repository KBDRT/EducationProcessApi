using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using MediatR;

namespace Application.CQRS.Analysis.Querires.GetCriteriasByTarget
{
    public class GetCriteriasByTargetQueryHandler : IRequestHandler<GetCriteriasByTargetQuery, CQResult<List<GetCriteriasWithOptionsDto>>>
    {
        private readonly IAnalysisRepository _analysisRepository;

        public GetCriteriasByTargetQueryHandler(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<CQResult<List<GetCriteriasWithOptionsDto>>> Handle(GetCriteriasByTargetQuery request, CancellationToken cancellationToken)
        {
            var serviceResult = new CQResult<List<GetCriteriasWithOptionsDto>>();
            var criterias = await _analysisRepository.GetByTargetAsync(request.Target);

            List<GetCriteriasWithOptionsDto> outputCriterias = [];
            serviceResult.SetResultData(outputCriterias);

            if (criterias != null)
            {
                foreach (var criteria in criterias)
                {
                    List<GetOptionsDto> options = new List<GetOptionsDto>();

                    foreach (var option in criteria.Options)
                    {
                        options.Add(new(option.Name, option.Id));
                    }

                    outputCriterias.Add(new(criteria.Name, criteria.Description, criteria.Id, options));
                }
            }
            else
            {
                serviceResult.AddMessage("Criteria не найден", "criteriaId");
            }

            return serviceResult;
        }
    }
}
