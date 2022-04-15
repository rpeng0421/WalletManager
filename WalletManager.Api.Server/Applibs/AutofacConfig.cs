using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using MongoDB.Driver;
using NLog;
using StackExchange.Redis;
using WalletManager.Ap.Model;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Persistent.MongoRepository;
using WalletManager.Persistent.RedisRepository;
using WalletManager.Persistent.Repository;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Api.Server.Applibs
{
    public class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null) Register();

                return container;
            }
        }

        private static void Register()
        {
            var builder = new ContainerBuilder();
            var asm = Assembly.GetExecutingAssembly();
            builder.RegisterApiControllers(asm);

            builder.RegisterType<RabbitMqFactory>()
                .WithParameter("rabbitUrl", ConfigHelper.RabbitMqUri)
                .WithParameter("rmqExpiration", ConfigHelper.RmqExpiration)
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterType<WalletFactory>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Ap"))
                .Where(t => t.IsAssignableTo<IPublisher>())
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();


            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Ap"))
                .Where(t => t.IsAssignableTo<IApplication>())
                .WithProperty("Logger", LogManager.GetLogger("WalletManager.Api.Server"))
                .As(t => t)
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(asm)
                .AssignableTo<IConsumerHandler<EventData>>()
                .Keyed<IConsumerHandler<EventData>>(x => x.Name.Replace("Consumer", ""))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            // sql ioc
            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Persistent"),
                    Assembly.Load("WalletManager.Domain"))
                .WithParameter("connStr", ConfigHelper.ConnectionString)
                .Where(t => t.Namespace == "WalletManager.Persistent.Repository")
                .Where(t => t.IsAssignableTo<ISqlRepository>())
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Persistent"),
                    Assembly.Load("WalletManager.Domain"))
                .WithParameter("client", new MongoClient(ConfigHelper.MongoConnStr))
                .Where(t => t.IsAssignableTo<IMongoRepository>())
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Persistent"),
                    Assembly.Load("WalletManager.Domain"))
                .WithParameter("connection", ConnectionMultiplexer.Connect(ConfigHelper.RedisConnStr))
                .Where(t => t.IsAssignableTo<IRedisRepository>())
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }
    }
}