using System.Threading.Tasks;
using HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions;
using HexMaster.Keesz.Live.IntegrationEvents.Events;
using HexMaster.Keesz.Live.SignalRHubs;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.Keesz.Live.IntegrationEvents.Handlers
{
    public class FriendInvitationReceivedEventHandler : IIntegrationEventHandler<FriendInvitationReceivedEvent>
    {

        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<UpdateHub> _updateHubContext;

        public FriendInvitationReceivedEventHandler(IHubContext<NotificationHub> notificationHubContext,
            IHubContext<UpdateHub> updateHubContext)
        {
            _notificationHubContext = notificationHubContext;
            _updateHubContext = updateHubContext;
        }

        public async Task Handle(FriendInvitationReceivedEvent @event)
        {
            var notificationHub = new NotificationHub(_notificationHubContext);
            var updateHub = new UpdateHub(_updateHubContext);

            await notificationHub.BroadcastNotification(@event.UserId, "Friend invitation received",
                $"User {@event.FriendName} sent you an invitation to become friends");

            await updateHub.UpdateFriendRequests(@event.UserId);
        }
    }
}
