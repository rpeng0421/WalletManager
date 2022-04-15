namespace WalletManager.RabbitMq.Model
{
    public abstract class IPublisher
    {
        protected string Topic { get; set; }

        protected RabbitMqFactory RabbitMqFactory { get; set; }

        protected IPublisher(string topic, RabbitMqFactory RabbitMqFactory)
        {
            this.Topic = topic;
            this.RabbitMqFactory = RabbitMqFactory;
        }
    }
}