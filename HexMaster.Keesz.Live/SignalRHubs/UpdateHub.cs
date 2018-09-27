using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.Keesz.Live.SignalRHubs
{
    public class UpdateHub : Hub
    {

        protected IHubContext<UpdateHub> _context;

        public UpdateHub(IHubContext<UpdateHub> context)
        {
            _context = context;
        }

        public async Task UpdateFriends(Guid userId)
        {
            await _context.Clients.Group(userId.ToString()).SendAsync("updateFriends");
        }
        public async Task UpdateFriendRequests(Guid userId)
        {
            await _context.Clients.Group(userId.ToString()).SendAsync("updateFriendRequests");
        }
        public async Task RegisterUserId(string userId)
        {
            await _context.Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

    }
}
