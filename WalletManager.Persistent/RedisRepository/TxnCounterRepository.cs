using System;
using StackExchange.Redis;
using WalletManager.Domain.Model.DepositReport;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.RedisRepository
{
    public class TxnCounterRepository : ITxnCounterRepository
    {
        private const string AffixKey = "TxnCounterRepository";
        private ConnectionMultiplexer conn;

        public (Exception exception, TxnReport txnReport) AddTxn(int walletId, string countType, decimal amount)
        {
            var scriptResult = UseConnection((redis) =>
            {
                var keys = new RedisKey[] {$"{AffixKey}:{walletId}.{countType}"};
                var values = new RedisValue[]
                {
                    amount.ToString()
                };
                string script =
                    @"
                    redis.call('HINCRBY', KEYS[1]
                    , 'Amount', ARGV[1])
                    redis.call('HINCRBY', KEYS[1]
                    , 'Count', 1)
                    return 1";
                return (bool) redis.ScriptEvaluate(script, keys, values);
            });
            if (!scriptResult)
            {
                return (new Exception($"{this.GetType().FullName} exec redis script fail"), null);
            }

            return this.FindWallet(walletId);
        }

        public (Exception exception, TxnReport txnReport) FindWallet(int walletId)
        {
            return (null, null);
        }

        private T UseConnection<T>(Func<IDatabase, T> func)
        {
            var redis = conn.GetDatabase(2);
            return func(redis);
        }
    }
}