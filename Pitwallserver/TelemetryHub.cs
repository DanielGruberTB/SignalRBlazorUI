using Microsoft.AspNetCore.SignalR;

namespace Pitwallserver
{
    public class TelemetryHub : Hub<ITelemetryHubClient>
    {
        public override Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).ReceiveNotification($"Connected to Telemetry Hub {Context.User?.Identity?.Name}");
            return base.OnConnectedAsync();
        }
    }

    public interface ITelemetryHubClient
    {
        Task ReceiveNotification(string message);
    }
}
