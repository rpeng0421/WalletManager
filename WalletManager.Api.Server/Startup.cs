using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using Owin;
using RabbitMQ.Client;
using WalletManager.Ap.Applibs;
using WalletManager.Ap.NoSqlService;
using WalletManager.Api.Server.Applibs;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Api.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);

            app.UseCors(CorsOptions.AllowAll);
        }

        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});

            //// API DI設定
            config.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacConfig.Container);
            RedisLockFactory.Connect(ConfigHelper.RedisConnStr);
            var rabbitMqFactory = AutofacConfig.Container.Resolve<RabbitMqFactory>();
            rabbitMqFactory.Connect();
            var eventDispatcher = AutofacConfig.Container.Resolve<EventDispatcher>();
            rabbitMqFactory.NewConsumer(
                eventDispatcher,
                "BalanceChange",
                "WalletManager.Api.Server",
                ExchangeType.Direct);


            return config;
        }
    }
}