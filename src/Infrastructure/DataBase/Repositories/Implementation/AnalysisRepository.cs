using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using EducationProcessAPI.Domain.Entities.LessonAnalyze;
using EducationProcessAPI.Application.Abstractions.Repositories;
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

        public async Task<Guid> CreateCriteriaAsync(AnalysisCriteria newCriteria)
        {
            _context.AnalyzeCriterions.Add(newCriteria);
            await _context.SaveChangesAsync();
            return newCriteria.Id;
        }

        public async Task<Guid> CreateOptionAsync(CriterionOption newOption)
        {
            _context.CriterionOptions.Add(newOption);
            await _context.SaveChangesAsync();
            return newOption.Id;
        }

        public async Task<List<AnalysisCriteria>?> GetByTargetAsync(AnalysisTarget target)
        {
            var criterias = await _context.AnalyzeCriterions
                                        .Where(x => x.AnalysisTarget == target)
                                        .Include(y => y.Options)
                                        .ToListAsync();

            return criterias;
        }

        public async Task<AnalysisCriteria?> GetCriteriaByIdAsync(Guid id)
        {
            var criteria = await _context.AnalyzeCriterions
                                      //.AsNoTracking()
                                      .SingleOrDefaultAsync(i => i.Id == id) ?? null;

            return criteria;
        }
    }
}
