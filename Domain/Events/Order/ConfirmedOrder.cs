namespace Domain.Events.Order
{
    public class ConfirmedOrder : IOrderEvent
    {
        public Guid OrderId { get; set ; }
        public DateTime OccurredOn { get; set; }
        public string EventType { get; set; }

        public ConfirmedOrder(Guid orderId)
        {
            OrderId = orderId;
            OccurredOn = DateTime.UtcNow;
            EventType = nameof(ConfirmedOrder);

        }
    }
}
