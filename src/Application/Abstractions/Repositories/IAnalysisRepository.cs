
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IAnalysisRepository
    {
        public Task<Guid> CreateCriteriaAsync(AnalysisCriteria newCriteria);

        public Task<Guid> CreateOptionAsync(CriterionOption newOption);

        public Task<AnalysisCriteria?> GetCriteriaByIdAsync(Guid id);

        public Task<List<AnalysisCriteria>?> GetByTargetAsync(AnalysisTarget target);


    }
}
