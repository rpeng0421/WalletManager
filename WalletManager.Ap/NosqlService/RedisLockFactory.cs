using System;
using System.Collections.Generic;
using NLog;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace WalletManager.Ap.NosqlService
{
    public class RedisLockFactory
    {
        private static ILogger logger = LogManager.GetLogger("RedisLockFactory");
        private static List<RedLockMultiplexer> connections = new List<RedLockMultiplexer>();
        private static RedLockFactory redFactory;

        public static IRedLock GetLock(string key)
        {
            var expire = TimeSpan.FromSeconds(30); //lock object 失效時間
            var wait = TimeSpan.FromSeconds(10); //放棄重試時間
            var retry = TimeSpan.FromSeconds(1); //重試間隔時間
            return redFactory.CreateLock(key, expire, wait, retry);
        }

        public static void Connect(string connStr)
        {
            var muxer = ConnectionMultiplexer.Connect(connStr);
            muxer.ConnectionFailed += (sender, args) =>
            {
                logger.Error("redis failed: " + EndPointCollection.ToString(args.EndPoint) + "/" +
                             args.ConnectionType);
            };
            muxer.ConnectionRestored += (sender, e) =>
            {
                logger.Error("redis restored: " + EndPointCollection.ToString(e.EndPoint) + "/" +
                             e.ConnectionType);
            };
            connections.Add(muxer);

            redFactory = RedLockFactory.Create(connections);
        }
    }
}