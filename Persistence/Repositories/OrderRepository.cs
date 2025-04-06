using Domain.Aggregates;
using Persistence.EventStore;

namespace Persistence.Repositories
{
    public class OrderRepository
    {
        private readonly IEventStore _eventStore;

        public OrderRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Order> GetByIdAsync(Guid orderId)
        {
            // Obtenemos los eventos históricos
            var events = await _eventStore.GetEventsAsync(orderId);
            if (!events.Any())
            {
                return null; // No existe ese order
            }

            // Reconstruimos la entidad (agregado) usando la lista de eventos
            var order = Order.Rebuild(events);
            return order;
        }

        public async Task SaveAsync(Order order)
        {
            // Tomamos los eventos nuevos (sin commitear)
            var uncommittedEvents = order.UncommittedEvents;
            if (uncommittedEvents.Any())
            {
                // Guardamos los eventos en el Event Store
                await _eventStore.SaveEventsAsync(order.Id, uncommittedEvents);

                // Limpiamos la lista de eventos
                order.ClearUncommittedEvents();
            }
        }
    }

}
