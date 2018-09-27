using System;
using HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Connect.IntegrationEvents
{
    public class KeeszConnectIntegrationEventService : IKeeszConnectIntegrationEventService
    {

        private readonly IEventBus _eventBus;

        public KeeszConnectIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
        }

    }
}
