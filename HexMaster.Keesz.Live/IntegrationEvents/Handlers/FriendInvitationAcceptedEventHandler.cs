using System.Threading.Tasks;
using HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions;
using HexMaster.Keesz.Live.IntegrationEvents.Events;
using HexMaster.Keesz.Live.SignalRHubs;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.Keesz.Live.IntegrationEvents.Handlers
{
    public class FriendInvitationAcceptedEventHandler : IIntegrationEventHandler<FriendInvitationAcceptedEvent>
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<UpdateHub> _updateHubContext;

        public FriendInvitationAcceptedEventHandler(IHubContext<NotificationHub> notificationHubContext,
            IHubContext<UpdateHub> updateHubContext)
        {
            _notificationHubContext = notificationHubContext;
            _updateHubContext = updateHubContext;
        }


        public async Task Handle(FriendInvitationAcceptedEvent @event)
        {

            var notificationHub = new NotificationHub(_notificationHubContext);
            var updateHub = new UpdateHub(_updateHubContext);

            await notificationHub.BroadcastNotification(@event.UserId, "Invitation accepted",
                $"User {@event.FriendName} is now a friend");

            await updateHub.UpdateFriends(@event.UserId);

        }
    }
}
