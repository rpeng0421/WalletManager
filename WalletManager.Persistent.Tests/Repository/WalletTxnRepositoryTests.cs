using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;
using WalletManager.Persistent.Repository;

namespace WalletManager.Persistent.Tests.Repository
{
    [TestClass]
    public class WalletTxnRepositoryTests
    {
        private const string connStr = @"Data Source=localhost;database=WalletManager;Integrated Security=True";
        private IWalletTxnRepository repo;

        [TestInitialize]
        public void Init()
        {
            var tracateSql = @"TRUNCATE TABLE t_wallet_txn";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Execute(tracateSql);
            }

            repo = new WalletTxnRepository(connStr);
        }

        [TestMethod]
        public void InsertBatch()
        {
            var insertResult = repo.Insert(
                Enumerable.Range(1, 5).Select(i => new WalletTxnPo
                {
                    f_id = i,
                    f_walletId = i % 2,
                    f_amount = 10,
                    f_balance = 20,
                    f_createdAt = DateTime.Now
                })
            );
            Assert.IsNull(insertResult.exception);
            Assert.AreEqual(insertResult.walletTxnPos.Count(), 5);
        }

        [TestMethod]
        public void Query()
        {
            var insertResult = repo.Insert(
                Enumerable.Range(1, 5).Select(i => new WalletTxnPo
                {
                    f_id = i,
                    f_walletId = i % 2,
                    f_amount = 10,
                    f_balance = 20,
                    f_createdAt = DateTime.Now
                })
            );
            Assert.IsNull(insertResult.exception);
            var queryResult = repo.Query(null);
            Assert.IsNull(queryResult.exception);
            Assert.AreEqual(queryResult.walletTxnPos.Count(), 5);
        }
    }
}