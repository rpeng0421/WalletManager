namespace WalletManager.RabbitMq.Model
{
    public interface IDispatcher<TEvent> where TEvent : DomainEvent
    {
        string ServiceName { get; set; }
        string EventType { get; set; }
        string HandlerType { get; set; }
        bool Dispatch(TEvent eventData);
    }
}