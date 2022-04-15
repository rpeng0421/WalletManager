using Newtonsoft.Json;
using WalletManager.Domain.Event;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Ap.Event
{
    public class BalanceChangePublisher : IPublisher
    {
        public BalanceChangePublisher(RabbitMqFactory rabbitMqFactory) : base("BalanceChange", rabbitMqFactory)
        {
        }

        public void Publish(BalanceChangeEvent eventData)
        {
            RabbitMqFactory.PublishDirect(
                Topic,
                new EventData(nameof(BalanceChangeEvent), JsonConvert.SerializeObject(eventData))
            );
        }
    }
}