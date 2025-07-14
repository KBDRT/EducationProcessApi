using Application.Services.Helpers.Definition;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Background
{
    public class StatisticsFormerBackgroundService : IHostedService, IDisposable
    {
        private readonly ILogger<StatisticsFormerBackgroundService> _logger;
        private Timer? _timer = null;
        private IServiceScopeFactory _serviceScopeFactory;

        private readonly TimeSpan _FREQUENCY_TIMER = TimeSpan.FromDays(1);

        public StatisticsFormerBackgroundService(ILogger<StatisticsFormerBackgroundService> logger, 
                                                 IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

       
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Counter BackgroundService started!");
            _timer = new Timer(StartService, null, TimeSpan.Zero, _FREQUENCY_TIMER);

            return Task.CompletedTask;
        }

        private void StartService(object? state)
        {
            _logger.LogInformation("Counter BackgroundService working!");
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            { 
                var statisticService =
                   scope.ServiceProvider.GetRequiredService<StatisticService>();
                statisticService.FillStats();
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Counter BackgroundService stopped!");
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}