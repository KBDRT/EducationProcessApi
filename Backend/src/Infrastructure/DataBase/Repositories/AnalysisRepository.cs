using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities.Analysis;
using Domain.Entities.Auth;
using EducationProcessAPI.Application.Abstractions.Repositories;
using EducationProcessAPI.Domain.Entities;
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

        public async Task<List<AnalysisCriteria>?> GetCriteriasByOptionsIdAsync(List<Guid> optionsId, CancellationToken cancellationToken = default)
        {
            return await _context.AnalyzeCriterions
                                        .Where(p => p.Options.Any(opt => optionsId.Contains(opt.Id)))
                                        .Include(y => y.Options.Where(z => optionsId.Contains(z.Id)))
                                        .OrderBy(x => x.Order)
                                        .ToListAsync(cancellationToken);
        }

        public async Task<AnalysisDocument?> GetDocumentByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
            return await _context.AnalysisDocuments
                                 .Include(y => y.SelectedOptions)
                                 .ThenInclude(y => y.Criterion)
                                 .Include(y => y.ArtUnion)
                                 .Include(y => y.Lesson)
                                 .Include(y => y.Teacher)
                                 .SingleOrDefaultAsync(x => x.Id == documentId, cancellationToken);
                                 
        }

        public async Task<Guid> GetFileIdForDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
        {
           return await _context.AnalysisDocuments
                                    .Where(x => x.Id == documentId)
                                    .Select(x => x.FileId)       
                                    .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task SetFileForDocumentAsync(Guid documentId, Guid fileId, CancellationToken cancellationToken = default)
        {
            await _context.AnalysisDocuments.Where(t => t.Id == documentId)
                                   .ExecuteUpdateAsync(u => u
                                   .SetProperty(f => f.FileId, fileId), cancellationToken: cancellationToken);
        }
    }
}
