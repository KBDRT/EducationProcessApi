using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Analysis;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using Microsoft.EntityFrameworkCore;

namespace EducationProcessAPI.Infrastructure.DataBase.Repositories.Implementation
{
    public class AnalysisRepository : IAnalysisRepository
    {

        private readonly ApplicationContext _context;

        public AnalysisRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateCriteriaAsync(AnalysisCriteria newCriteria, CancellationToken cancellationToken = default)
        {
            _context.AnalyzeCriterions.Add(newCriteria);
            await _context.SaveChangesAsync(cancellationToken);
            return newCriteria.Id;
        }

        public async Task<Guid> CreateDocumentAsync(AnalysisDocument document, CancellationToken cancellationToken = default)
        {
            _context.AnalysisDocuments.Add(document);
            await _context.SaveChangesAsync(cancellationToken);
            return document.Id;
        }

        public async Task<Guid> CreateOptionAsync(CriterionOption newOption, CancellationToken cancellationToken = default)
        {
            _context.CriterionOptions.Add(newOption);
            await _context.SaveChangesAsync(cancellationToken);
            return newOption.Id;
        }

        public async Task CreateRangeAsync(List<AnalysisCriteria> criterias, CancellationToken cancellationToken = default)
        {
            _context.AnalyzeCriterions.AddRange(criterias);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteByTargetAsync(AnalysisTarget target, CancellationToken cancellationToken = default)
        {
            await _context.AnalyzeCriterions.Where(x => x.AnalysisTarget == target).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<List<AnalysisCriteria>?> GetByTargetAsync(AnalysisTarget target, CancellationToken cancellationToken = default)
        {
            var criterias = await _context.AnalyzeCriterions
                                        .Where(x => x.AnalysisTarget == target)
                                        .Include(y => y.Options)
                                        .OrderBy(x => x.Order)
                                        .ToListAsync(cancellationToken);

            return criterias;
        }

        public async Task<AnalysisCriteria?> GetCriteriaByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var criteria = await _context.AnalyzeCriterions
                                      //.AsNoTracking()
                                      .SingleOrDefaultAsync(i => i.Id == id, cancellationToken) ?? null;

            return criteria;
        }
    }
}
