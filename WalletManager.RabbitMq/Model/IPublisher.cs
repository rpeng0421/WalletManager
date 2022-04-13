namespace WalletManager.RabbitMq.Model
{
    public interface IPublisher
    {
        string Topic { get; set; }
        string ExchangeType { get; set; }

        void Publish<T>(T eventData) where T : EventData;
    }
}