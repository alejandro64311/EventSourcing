using Domain.Events;

namespace Persistence.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<IOrderEvent>> GetEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<IOrderEvent> events);
    }
}
