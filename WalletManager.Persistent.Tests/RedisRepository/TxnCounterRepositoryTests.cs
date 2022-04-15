using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using WalletManager.Domain.Repository;
using WalletManager.Persistent.RedisRepository;

namespace WalletManager.Persistent.Tests.RedisRepository
{
    [TestClass]
    public class TxnCounterRepositoryTests
    {
        private const string redisConnStr = "localhost:6379";
        private ITxnCounterRepository counterRepository;

        [TestInitialize]
        public void Init()
        {
            var conn = ConnectionMultiplexer.Connect(redisConnStr);
            this.counterRepository = new TxnCounterRepository(conn);
            var db = conn.GetDatabase(2);
            db.Execute("FLUSHDB");
        }

        [TestMethod]
        public void AddTxn()
        {
            var addResult = this.counterRepository.AddTxn(1, "Deposit", Convert.ToDecimal(20.1));
            Assert.IsNull(addResult.exception);
            addResult = this.counterRepository.AddTxn(1, "Deposit", Convert.ToDecimal(20.1));
            Assert.AreEqual(addResult.txnReport.Amount, Convert.ToDecimal(40.2));
        }
    }
}