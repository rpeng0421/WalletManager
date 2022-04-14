using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using NLog;
using WalletManager.Ap.Model;
using WalletManager.Domain.Model;

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

            builder.RegisterType<WalletFactory>()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Ap"))
                .Where(t => t.IsAssignableTo<IApplication>())
                .WithProperty("Logger", LogManager.GetLogger("WalletManager.Api.Server"))
                .As(t => t)
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            // sql ioc
            builder.RegisterAssemblyTypes(Assembly.Load("WalletManager.Persistent"),
                    Assembly.Load("WalletManager.Domain"))
                .WithParameter("connStr", ConfigHelper.ConnectionString)
                .Where(t => t.Namespace == "WalletManager.Persistent.Repository" ||
                            t.Namespace == "WalletManager.Domain.Repository")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == $"I{t.Name}"))
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .SingleInstance();

            container = builder.Build();
        }
    }
}