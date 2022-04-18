using Newtonsoft.Json;
using WalletManager.Domain.Helper;

namespace WalletManager.RabbitMq.Model
{
    public class DomainEvent
    {
        public DomainEvent(string type, string data)
        {
            this.Type = type;
            Timestamp = TimestampHelper.Now();
            Data = data;
        }

        public string Type { get; set; }
        public long Timestamp { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}