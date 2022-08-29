using Microsoft.AspNetCore.SignalR;

namespace TrainCarAPI.Services
{
    public interface ISignalRNotificationService
    {
        Task SendMessageAsync<T>(T message) where T : class;
    }

    public class SignalRNotificationService : ISignalRNotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync<T>(T message) where T : class
        {
            await _hubContext.Clients.All.SendAsync("sendMessage", message);
        }
    }
}
