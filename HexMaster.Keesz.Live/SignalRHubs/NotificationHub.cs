using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.Keesz.Live.SignalRHubs
{
    public class NotificationHub : Hub
    {

        protected IHubContext<NotificationHub> _context;

        public NotificationHub(IHubContext<NotificationHub> context)
        {
            _context = context;
        }

        public async Task BroadcastNotification(Guid userId, string headline, string message)
        {
            await _context.Clients.Group(userId.ToString()).SendAsync("notificationReceived", headline, message);
        }
        public async Task RegisterUserId(string userId)
        {
            await _context.Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

    }
}
