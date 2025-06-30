
using Domain.Entities.Analysis;
using EducationProcessAPI.Domain.Entities;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IAnalysisRepository
    {
        public Task<Guid> CreateCriteriaAsync(AnalysisCriteria newCriteria);

        public Task<Guid> CreateOptionAsync(CriterionOption newOption);

        public Task<AnalysisCriteria?> GetCriteriaByIdAsync(Guid id);

        public Task<List<AnalysisCriteria>?> GetByTargetAsync(AnalysisTarget target);

        public Task CreateRangeAsync(List<AnalysisCriteria> criterias);

        public Task DeleteByTargetAsync(AnalysisTarget target);

        public Task<Guid> CreateDocumentAsync(AnalysisDocument document);

    }
}
