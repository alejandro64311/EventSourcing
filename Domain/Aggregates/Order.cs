using Domain.Events;
using Domain.Events.Order;

namespace Domain.Aggregates
{
    public class Order:  BaseAggregate<Order>
    {
        public Guid Id { get; private set; }
        public string Customer { get; private set; }
        public bool Confirmed { get; private set; }
        public List<(string product, int quantity)> Items { get; private set; }


        public Order()
        {

            Items = new List<(string, int)>();
        }

        public static Order Create(Guid orderId, string customer)
        {
            var order = new Order();
            var evt = new CreatedOrder(orderId, customer);
            order.Apply(evt);
            return order;
        }

        public void AddProduct(string product, int quantity)
        {
            var evt = new AddedProduct(this.Id, product, quantity);
            Apply(evt);
        }

        public void Confirm()
        {
            var evt = new ConfirmedOrder(this.Id);
            Apply(evt);
        }

        protected override void Apply(IEvent evt, bool isReplaying = false)
        {
            switch (evt)
            {
                case CreatedOrder e:
                    Id = e.OrderId;
                    Customer = e.Customer;
                    Confirmed = false;
                    Items = new List<(string, int)>();
                    break;

                case AddedProduct e:
                    Items.Add((e.ProductName, e.Quantity));
                    break;

                case ConfirmedOrder e:
                    Confirmed = true;
                    break;
            }

            if (!isReplaying)
            {
                _uncommittedEvents.Add(evt);
            }
        }

       
    }

}
