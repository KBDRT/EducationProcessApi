using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Application.Services.CRUD.Definition;
using EducationProcessAPI.Application.Services.Helpers.Definition;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using EducationProcessAPI.Application.Abstractions.Repositories;
using System.Collections.Generic;

namespace EducationProcessAPI.Application.Services.CRUD.Implementation
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IAnalysisRepository _analysisRepository;

        public AnalysisService(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<Guid> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto)
        {
            AnalysisCriteria criteria = new AnalysisCriteria()
            {
                Id = Guid.NewGuid(),
                AnalysisTarget = criteriaDto.AnalysisTarget,
                Description = criteriaDto.Description ?? string.Empty,
                Name = criteriaDto.Name,
                Order = criteriaDto.Order,
                WordMark = criteriaDto.WordMark,
            };

            return await _analysisRepository.CreateCriteriaAsync(criteria);
        }

        public async Task<Guid> CreateOptionAsync(Guid criteriaId, string name)
        {
            var criteria = await _analysisRepository.GetCriteriaByIdAsync(criteriaId);

            if (criteria == null)
            {
                return Guid.Empty;
            }    

            CriterionOption option = new CriterionOption()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Criterion = criteria
            };

            return await _analysisRepository.CreateOptionAsync(option);
        }

        public async Task<List<GetCriteriasWithOptionsDto>> GetByTargetAsync(AnalysisTarget target)
        {
            var criterias = await _analysisRepository.GetByTargetAsync(target);

            List <GetCriteriasWithOptionsDto> outputCriterias = new List<GetCriteriasWithOptionsDto>();

            if (criterias == null)
            {
                return outputCriterias;
            }


            criterias = criterias?.OrderBy(c => c.Order).ToList();

            foreach (var criteria in criterias)
            {
                List<GetOptionsDto> options = new List<GetOptionsDto>();

                foreach (var option in criteria.Options)
                {
                    options.Add(new(option.Name));
                }

                outputCriterias.Add(new(criteria.Name, criteria.Description, options));
            }

            return outputCriterias;
        }
    }
}
