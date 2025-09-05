using MassTransit;

namespace BuildingBlocks.Message.Events;

[ExcludeFromTopology]
public interface IIntegrationEvent : IEvent
{
}