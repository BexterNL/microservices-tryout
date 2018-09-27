using System;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Connect.IntegrationEvents.Events
{
    public class FriendInvitationAcceptedEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public string FriendName { get; set; }

        public FriendInvitationAcceptedEvent(Guid userId, Guid friendId, string friendName)
        {
            UserId = userId;
            FriendId = friendId;
            FriendName = friendName;
        }
    }
}
