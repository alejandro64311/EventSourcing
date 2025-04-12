using Domain.Events;
using Domain.Events.Order;

namespace Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IEvent>> _eventStream = new Dictionary<Guid, List<IEvent>>();

        public Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId)
        {
            if (_eventStream.ContainsKey(aggregateId))
            {
                return Task.FromResult(_eventStream[aggregateId].AsEnumerable());
            }
            return Task.FromResult(Enumerable.Empty<IEvent>());
        }

        public Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events)
        {
            if (!_eventStream.ContainsKey(aggregateId))
            {
                _eventStream[aggregateId] = new List<IEvent>();
            }

            _eventStream[aggregateId].AddRange(events);
            return Task.CompletedTask;
        }
    }

}
