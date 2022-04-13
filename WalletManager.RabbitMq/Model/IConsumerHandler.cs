namespace WalletManager.RabbitMq.Model
{
    public interface IConsumerHandler<TEvent> where TEvent : EventData
    {
        bool Handle(TEvent eventData);
    }
}