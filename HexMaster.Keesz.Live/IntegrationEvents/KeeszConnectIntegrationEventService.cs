using System;
using HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Live.IntegrationEvents
{
    public class KeeszRealtimeIntegrationEventService : IKeeszRealtimeIntegrationEventService
    {

        private readonly IEventBus _eventBus;

        public KeeszRealtimeIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
        }

    }
}
