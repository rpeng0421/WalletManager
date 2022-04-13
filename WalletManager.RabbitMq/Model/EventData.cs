using Newtonsoft.Json;
using WalletManager.Domain.Helper;

namespace WalletManager.RabbitMq.Model
{
    public class EventData
    {
        public string Type { get; set; }
        public long Timestamp { get; set; }
        public string Data { get; set; }

        public EventData(string data)
        {
            Timestamp = TimestampHelper.Now();
            Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}