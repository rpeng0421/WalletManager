using System;
using System.Collections.Generic;
using StackExchange.Redis;
using WalletManager.Domain.Model.DepositReport;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.RedisRepository
{
    public class TxnCounterRepository : ITxnCounterRepository, IRedisRepository
    {
        private const string AffixKey = "TxnCounterRepository";
        private ConnectionMultiplexer connection;

        public TxnCounterRepository(ConnectionMultiplexer connection)
        {
            this.connection = connection;
        }

        public (Exception exception, TxnReport txnReport) AddTxn(int walletId, string countType, decimal amount)
        {
            var scriptResult = UseConnection((redis) =>
            {
                var keys = new RedisKey[] {getHKey(walletId, countType)};
                var values = new RedisValue[]
                {
                    amount.ToString()
                };
                string script =
                    @"
                    redis.call('HINCRBY', KEYS[1], 'Amount', ARGV[1])
                    redis.call('HINCRBY', KEYS[1], 'Count', 1)
                    return 1";
                return (bool) redis.ScriptEvaluate(script, keys, values);
            });
            if (!scriptResult)
            {
                return (new Exception($"{this.GetType().FullName} exec redis script fail"), null);
            }

            return this.FindWallet(walletId, countType);
        }

        public (Exception exception, TxnReport txnReport) FindWallet(int walletId, string countType)
        {
            var report = UseConnection(redis =>
            {
                var entrys = redis.HashGetAll(getHKey(walletId, countType));
                if (entrys.Length > 0)
                {
                    var dic = new Dictionary<string, RedisValue>();
                    foreach(var entry in entrys)
                    {
                        dic.Add(entry.Name, entry.Value);
                    }

                    return new TxnReport
                    {
                        WalletId = walletId,
                        TxnType = countType,
                        Amount = Convert.ToDecimal(dic["Amount"]),
                        Count = Convert.ToInt32(dic["Count"])
                    };
                }

                return null;
            });
            if (report == null)
            {
                return (new Exception($"redis not found record"), null);
            }

            return (null, report);
        }

        private T UseConnection<T>(Func<IDatabase, T> func)
        {
            var redis = connection.GetDatabase(2);
            return func(redis);
        }

        private string getHKey(int walletId, string countType)
        {
            return $"{AffixKey}.{countType}:{walletId}";
        }
    }
}