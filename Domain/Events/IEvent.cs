namespace Domain.Events
{
    public interface IEvent
    {
         DateTime OccurredOn { get; set; }
         string EventType { get; set; }
    }
}
