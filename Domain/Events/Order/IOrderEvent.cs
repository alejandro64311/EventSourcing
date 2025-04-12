namespace Domain.Events.Order
{
    public interface IOrderEvent : IEvent
    {
        Guid OrderId { get; set; }
    }
}
