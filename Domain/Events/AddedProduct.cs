

namespace Domain.Events
{
    public class AddedProduct: IOrderEvent
    {

        public string ProductName { get; }
        public int Quantity { get; }
        public Guid OrderId { get; set; }
        public DateTime OcurredOn { get; set; }
        

        public AddedProduct(Guid orderId, string productName, int quantity)
        {
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            OcurredOn = DateTime.UtcNow;
        }
    }
}
