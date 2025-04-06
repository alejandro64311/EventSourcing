

using Domain.Events;

namespace Persistence.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<IOrderEvent>> _eventStream = new Dictionary<Guid, List<IOrderEvent>>();

        public Task<IEnumerable<IOrderEvent>> GetEventsAsync(Guid aggregateId)
        {
            if (_eventStream.ContainsKey(aggregateId))
            {
                return Task.FromResult(_eventStream[aggregateId].AsEnumerable());
            }
            return Task.FromResult(Enumerable.Empty<IOrderEvent>());
        }

        public Task SaveEventsAsync(Guid aggregateId, IEnumerable<IOrderEvent> events)
        {
            if (!_eventStream.ContainsKey(aggregateId))
            {
                _eventStream[aggregateId] = new List<IOrderEvent>();
            }

            _eventStream[aggregateId].AddRange(events);
            return Task.CompletedTask;
        }
    }

}
