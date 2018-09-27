using System;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Connect.IntegrationEvents.Events
{
    public class FriendInvitationReceivedEvent : IntegrationEvent
    {

        public Guid UserId { get; set; }
        public string FriendName { get; set; }

        public FriendInvitationReceivedEvent(Guid userId, string friendName)
        {
            UserId = userId;
            FriendName = friendName;
        }

    }
}
