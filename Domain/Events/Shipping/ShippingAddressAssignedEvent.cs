

namespace Domain.Events.Shipping
{
    public class ShippingAddressAssignedEvent : IShippingEvent
    {
        public Guid Id { get;  set; }
        public DateTime OccurredOn { get;  set; }
        public string EventType { get;  set; }

        public Guid ShippingId { get;  set; }
        public string Address { get;  set; }

        public ShippingAddressAssignedEvent(Guid shippingId, string address)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
            EventType = nameof(ShippingAddressAssignedEvent);

            ShippingId = shippingId;
            Address = address;
        }
    }

}
