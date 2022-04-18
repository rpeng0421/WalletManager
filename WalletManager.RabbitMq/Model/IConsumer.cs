namespace WalletManager.RabbitMq.Model
{
    public interface IConsumer
    {
        bool Handle(DomainEvent domainEvent);
    }
}