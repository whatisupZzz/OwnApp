using BuildingBlocks.Core;
using BuildingBlocks.Message.Events;

namespace NotificationAPI
{
    public class NotificationEventMapper: IEventMapper
    {
        public IIntegrationEvent? MapToIntegrationEvent(IDomainEvent @event)
        {
            return @event switch
            {
                _ => null
            };
        }

        public IInternalCommand? MapToInternalCommand(IDomainEvent @event)
        {
            return @event switch
            {
                _ => null
            };
        }
    }
}
