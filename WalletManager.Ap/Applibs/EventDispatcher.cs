using System.Collections.Generic;
using Autofac.Features.Indexed;
using Newtonsoft.Json;
using NLog;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Ap.Applibs
{
    public class EventDispatcher : IDispatcher<DomainEvent>
    {
        private ILogger logger = LogManager.GetLogger("WalletManager.Api.Server");
        private IIndex<string, IConsumer> consumerSet;

        public EventDispatcher(IIndex<string, IConsumer> consumerSet)
        {
            this.consumerSet = consumerSet;
        }

        public string ServiceName { get; set; }
        public string EventType { get; set; }
        public string HandlerType { get; set; }

        public bool Dispatch(DomainEvent domainEvent)
        {
            logger.Debug($"get event data {JsonConvert.SerializeObject(domainEvent)}");
            var handlerName = domainEvent.Type.Replace("Event", "");
            if (this.consumerSet.TryGetValue(handlerName, out var handler))
            {
                handler.Handle(domainEvent);
                return true;
            }

            return false;
        }
    }
}