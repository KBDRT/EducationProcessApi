using Application.Abstractions.Repositories;
using Application.Services.Helpers.Definition;
using Domain.Entities.Stats;
using EducationProcessAPI.Domain.Entities;

namespace Application.Services.Helpers.Implementation
{
    public class TableStatisticService(IBaseRepository<Lesson> lessonRepository, 
                                       IStatisticsRepository statisticRepository) : StatisticService
    {

        private IBaseRepository<Lesson> _lessonRepository = lessonRepository;
        private IStatisticsRepository _statisticRepository = statisticRepository;

        private int _lessonCounter = 0;
        private GeneralStatistics? _lessonStats;

        protected override void GetInfo()
        {
            _lessonCounter = _lessonRepository.GetRecordsCount();
        }

        protected override void CalcStats()
        {
            _lessonStats = new()
            {
                Id = Guid.NewGuid(),
                RecalcDate = DateTime.Now,
                Target = StatisticsTarget.Lessons,
                TotalAmount = _lessonCounter,
            };
        }

        
        protected override void SaveStats()
        {
            if (_lessonStats != null)
            {
                _statisticRepository.UpdateStaticAsync(_lessonStats);
            }
        }
    }
}
