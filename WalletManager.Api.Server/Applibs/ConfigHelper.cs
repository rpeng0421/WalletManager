using System.Configuration;

namespace WalletManager.Api.Server.Applibs
{
    public class ConfigHelper
    {
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["WalletManager"].ConnectionString;


        public static readonly string RedisConnStr = ConfigurationManager.AppSettings["redisConnStr"];

        public static string ServiceUrl => "http://*:8085";
    }
}