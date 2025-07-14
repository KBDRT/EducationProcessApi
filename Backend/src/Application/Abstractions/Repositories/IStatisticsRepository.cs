using Domain.Entities.Stats;

namespace Application.Abstractions.Repositories
{
    public interface IStatisticsRepository
    {
        public Task UpdateStaticAsync(GeneralStatistics statistic);

    }
}
