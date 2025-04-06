


namespace Domain.Events
{
    public class ConfirmedOrder : IOrderEvent
    {
        public Guid OrderId { get; set ; }
        public DateTime OcurredOn { get; set; }
        public ConfirmedOrder(Guid orderId)
        {
            OrderId = orderId;
            OcurredOn = DateTime.UtcNow;
        }
    }
}
