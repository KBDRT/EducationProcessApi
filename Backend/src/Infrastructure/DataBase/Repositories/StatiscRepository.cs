using Application.Abstractions.Repositories;
using DocumentFormat.OpenXml.InkML;
using Domain.Entities.Stats;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataBase.Repositories
{
    public class StatiscRepository : IStatisticsRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StatiscRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


        public async Task UpdateStaticAsync(GeneralStatistics statistic)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var savedStat = await context.Statistics
                .FirstOrDefaultAsync(t => t.Target == statistic.Target);
            if (savedStat != null)
            {
                context.Entry(savedStat).State = EntityState.Modified;
                savedStat.RecalcDate = statistic.RecalcDate;
                savedStat.Target = statistic.Target;
            }
            else
            {
                await context.Statistics.AddAsync(statistic);
            }
            await context.SaveChangesAsync();
        }
    }
}
