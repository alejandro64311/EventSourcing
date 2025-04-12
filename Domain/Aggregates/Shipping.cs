using Domain.Events.Shipping;
using Domain.Events;


namespace Domain.Aggregates
{
    public class Shipping: BaseAggregate<Shipping>
    {

        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public string Address { get; private set; }
        public bool IsDelivered { get; private set; }


        public Shipping() { } 

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
        protected override void Apply(IEvent evt, bool isReplaying = false)
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
       
    }
}
