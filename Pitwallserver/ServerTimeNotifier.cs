
using Microsoft.AspNetCore.SignalR;

namespace Pitwallserver
{
    public class ServerTimeNotifier : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeNotifier> _logger;
        private readonly IHubContext<TelemetryHub, ITelemetryHubClient> _hubContext;

        public ServerTimeNotifier(ILogger<ServerTimeNotifier> logger, IHubContext<TelemetryHub,ITelemetryHubClient> context)
        {
            _logger = logger;
            _hubContext = context;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var dateTime = DateTime.Now;
                _logger.LogInformation("Executing {Service} {Time}", nameof(ServerTimeNotifier), dateTime);
                await _hubContext.Clients.All.ReceiveNotification($"Server time is {dateTime}");
            }
        }
    }
}
