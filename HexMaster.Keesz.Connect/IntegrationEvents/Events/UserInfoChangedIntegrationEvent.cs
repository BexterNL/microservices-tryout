using System;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Connect.IntegrationEvents.Events
{
    public class UserInfoChangedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public UserInfoChangedIntegrationEvent(Guid userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
        }

    }
}