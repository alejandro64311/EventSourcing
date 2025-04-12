namespace Domain.Events.Order
{
    public class CreatedOrder : IOrderEvent
    {
        public string Customer { get; }
        public Guid OrderId { get; set; }
        public DateTime OccurredOn { get; set; }
        public string EventType { get; set; }

        public CreatedOrder(Guid orderId, string customer)
        {
            OrderId = orderId;
            Customer = customer;
            OccurredOn = DateTime.UtcNow;
            EventType = nameof(CreatedOrder);

        }
    }
}
