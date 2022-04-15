using System.Collections.Generic;
using Autofac.Features.Indexed;
using Newtonsoft.Json;
using NLog;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Ap.Applibs
{
    public class EventDispatcher : IConsumerHandler<EventData>
    {
        private ILogger logger = LogManager.GetLogger("WalletManager.Api.Server");
        private IIndex<string, IConsumerHandler<EventData>> consumerSet;

        public EventDispatcher(IIndex<string, IConsumerHandler<EventData>> consumerSet)
        {
            this.consumerSet = consumerSet;
        }

        public bool Handle(EventData eventData)
        {
            logger.Debug($"get event data {JsonConvert.SerializeObject(eventData)}");
            var handlerName = eventData.Type.Replace("Event", "");
            if (this.consumerSet.TryGetValue(handlerName, out var handler))
            {
                handler.Handle(eventData);
                return true;
            }

            return false;
        }
    }
}