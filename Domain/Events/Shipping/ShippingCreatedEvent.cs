
namespace Domain.Events.Shipping
{
    public class ShippingCreatedEvent : IShippingEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredOn { get; set; }
        public string EventType { get;  set; }

        public Guid ShippingId { get;  set; }
        public Guid OrderId { get;  set; }


        public ShippingCreatedEvent(Guid shippingId, Guid orderId)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
            EventType = nameof(ShippingCreatedEvent);

            ShippingId = shippingId;
            OrderId = orderId;

        }
    }
}
