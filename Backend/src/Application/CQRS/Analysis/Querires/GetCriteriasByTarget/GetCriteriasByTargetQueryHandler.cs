using Application.CQRS.Result.CQResult;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
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

            if (criterias != null)
            {
                List<GetCriteriasWithOptionsDto> outputCriterias = CrateCriteriasList(criterias);
                serviceResult.SetResultData(outputCriterias);
            }
            else
            {
                serviceResult.AddMessage("Criteria не найден", "criteriaId");
            }

            return serviceResult;
        }

        private List<GetCriteriasWithOptionsDto> CrateCriteriasList(List<AnalysisCriteria> criterias)
        {
            List<GetCriteriasWithOptionsDto> outputCriterias = [];

            foreach (var criteria in criterias)
            {
                List<GetOptionsDto> options = new List<GetOptionsDto>();

                foreach (var option in criteria.Options)
                {
                    options.Add(new(option.Name, option.Id));
                }
                outputCriterias.Add(new(criteria.Name, criteria.Description, criteria.Id, options));
            }

            return outputCriterias;
        }

    }
}
