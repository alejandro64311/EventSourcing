

namespace Domain.Events
{
    public interface IOrderEvent
    {
         Guid OrderId { get; set; }
         DateTime OcurredOn { get; set; }
    }
}
