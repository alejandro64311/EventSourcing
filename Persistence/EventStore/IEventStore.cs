using Domain.Events;

namespace Persistence.EventStore
{
    public interface IEventStore
    {
        Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId);
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events);
    }
}
