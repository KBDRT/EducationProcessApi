
using EducationProcess.Presentation.Contracts;
using EducationProcessAPI.Application.DTO;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Application.Services.CRUD.Definition
{
    public interface IAnalysisService
    {
        public Task<Guid> CreateCriteriaAsync(CreateAnalysisCriteriaDto criteriaDto);

        public Task<Guid> CreateOptionAsync(Guid criteriaId, string name);

        public Task<List<GetCriteriasWithOptionsDto>> GetByTargetAsync(AnalysisTarget target);

    }



}
