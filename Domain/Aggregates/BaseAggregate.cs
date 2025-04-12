

using Domain.Events;

namespace Domain.Aggregates
{
    public abstract class BaseAggregate <T> where T : BaseAggregate<T>, new()

    {
        protected readonly List<IEvent> _uncommittedEvents = new List<IEvent>();
        public IReadOnlyCollection<IEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }
        protected abstract void Apply(IEvent evt, bool isReplaying = false);

        public static T Rehydrate(IEnumerable<IEvent> history)
        {
            T instance = new T();

            foreach (var evt in history)
            {
                instance.Apply(evt);
            }

            return instance;
        }

    }
}
