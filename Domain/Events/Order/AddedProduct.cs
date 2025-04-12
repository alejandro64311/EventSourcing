
namespace Domain.Events.Order
{
    public class AddedProduct: IOrderEvent
    {

        public string ProductName { get; }
        public int Quantity { get; }
        public Guid OrderId { get; set; }
        public DateTime OccurredOn { get; set; }
        public string EventType { get; set; }

        public AddedProduct(Guid orderId, string productName, int quantity)
        {
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            OccurredOn = DateTime.UtcNow;
            EventType = nameof(AddedProduct);
        }
    }
}
