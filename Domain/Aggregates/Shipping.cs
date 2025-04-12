using Domain.Events.Shipping;
using Domain.Events;
using Domain.Events.Order;


namespace Domain.Aggregates
{
    public class Shipping
    {


        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public string Address { get; private set; }
        public bool IsDelivered { get; private set; }

        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();
        public IReadOnlyCollection<IEvent> UncommittedEvents => _uncommittedEvents.AsReadOnly();

        private Shipping() { } 

        public static Shipping Create(Guid shippingId, Guid orderId)
        {
            var shipping = new Shipping();
            var evt = new ShippingCreatedEvent(shippingId, orderId);
            shipping.Apply(evt);
            return shipping;
        }

        public void AssignAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Dirección de envío no válida.");

            var evt = new ShippingAddressAssignedEvent(Id, address);
            Apply(evt);
        }

        private void Apply(IEvent evt, bool isReplaying = false)
        {
            switch (evt)
            {
                case ShippingCreatedEvent e:
                    Id = e.ShippingId;
                    OrderId = e.OrderId;
                    IsDelivered = false;
                    break;

                case ShippingAddressAssignedEvent e:
                    Address = e.Address;
                    break;
            }
            if (!isReplaying)
                _uncommittedEvents.Add(evt);
        }


        public static Shipping Rehydrate(IEnumerable<IEvent> history)
        {
            var shipping = new Shipping();
            foreach (var evt in history)
            {
                shipping.Apply(evt);
            }
            return shipping;
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

    }
}
