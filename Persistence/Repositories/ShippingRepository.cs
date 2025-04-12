using Domain.Aggregates;
using Persistence.EventStore;

namespace Persistence.Repositories
{
    public class ShippingRepository
    {
        private readonly IEventStore _eventStore;

        public ShippingRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Shipping> GetByIdAsync(Guid shippingId)
        {
            // Obtenemos los eventos históricos
            var events = await _eventStore.GetEventsAsync(shippingId);
            if (!events.Any())
            {
                return null; // No existe ese shipping
            }

            // Reconstruimos la entidad (agregado) usando la lista de eventos
            var shipping = Shipping.Rehydrate(events);
            return shipping;
        }

        public async Task SaveAsync(Shipping shipping)
        {
            // Tomamos los eventos nuevos (sin commitear)
            var uncommittedEvents = shipping.UncommittedEvents;
            if (uncommittedEvents.Any())
            {
                // Guardamos los eventos en el Event Store
                await _eventStore.SaveEventsAsync(shipping.Id, uncommittedEvents);

                // Limpiamos la lista de eventos
                shipping.ClearUncommittedEvents();
            }
        }
    }

}
