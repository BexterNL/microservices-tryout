using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Live.IntegrationEvents
{
    public interface IKeeszRealtimeIntegrationEventService
    {
        void PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}