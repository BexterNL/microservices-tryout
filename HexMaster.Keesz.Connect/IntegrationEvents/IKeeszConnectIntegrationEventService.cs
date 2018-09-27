using System.Threading.Tasks;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.Connect.IntegrationEvents
{
    public interface IKeeszConnectIntegrationEventService
    {
        void PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}