

namespace Domain.Events
{
    public class CreatedOrder : IOrderEvent
    {
        public string Customer { get; }
        public Guid OrderId { get ; set ; }
        public DateTime OcurredOn { get; set ; }

        public CreatedOrder(Guid orderId, string customer)
        {
            OrderId = orderId;
            Customer = customer;
            OcurredOn = DateTime.UtcNow;
        }
    }
}
