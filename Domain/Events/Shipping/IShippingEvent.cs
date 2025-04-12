namespace Domain.Events.Shipping
{
    public interface IShippingEvent : IEvent
    {
        Guid ShippingId { get; set; }
    }
}
