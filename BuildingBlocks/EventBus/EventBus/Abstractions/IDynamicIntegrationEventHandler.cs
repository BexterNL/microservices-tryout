using System.Threading.Tasks;

namespace HexMaster.Keesz.BuildingBlocks.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
