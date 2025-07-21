
using Domain.Entities.Analysis;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;

namespace EducationProcessAPI.Application.Abstractions.Repositories
{
    public interface IAnalysisRepository
    {
        public Task<Guid> CreateCriteriaAsync(AnalysisCriteria newCriteria, CancellationToken cancellationToken = default);

        public Task<Guid> CreateOptionAsync(CriterionOption newOption, CancellationToken cancellationToken = default);

        public Task<AnalysisCriteria?> GetCriteriaByIdAsync(Guid id, CancellationToken cancellationToken = default);

        public Task<List<AnalysisCriteria>?> GetCriteriasByOptionsIdAsync(List<Guid> optionsId, CancellationToken cancellationToken = default);

        public Task<List<AnalysisCriteria>?> GetByTargetAsync(AnalysisTarget target, CancellationToken cancellationToken = default);

        public Task CreateRangeAsync(List<AnalysisCriteria> criterias, CancellationToken cancellationToken = default);

        public Task DeleteByTargetAsync(AnalysisTarget target, CancellationToken cancellationToken = default);

        public Task<Guid> CreateDocumentAsync(AnalysisDocument document, CancellationToken cancellationToken = default);

        public Task<AnalysisDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default);

        public Task SetFileForDocumentAsync(Guid documentId, Guid fileId, CancellationToken cancellationToken = default);

        public Task<Guid> GetFileIdForDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);

        public Task<List<AnalysisDocument>?> GetDocumentsAsync(int page, int size, CancellationToken cancellationToken = default);
    }
}
