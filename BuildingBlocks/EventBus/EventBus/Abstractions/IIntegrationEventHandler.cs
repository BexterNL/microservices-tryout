using System.Threading.Tasks;
using HexMaster.Keesz.BuildingBlocks.EventBus.Events;

namespace HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler 
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
