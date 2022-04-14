using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WalletManager.Domain.Repository;
using WalletManager.Persistent.Repository;
using Dapper;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Persistent.Tests.Repository
{
    [TestClass]
    public class WalletRepositoryTests
    {
        private const string connStr = @"Data Source=localhost;database=WalletManager;Integrated Security=True";
        private IWalletRepository repo;

        [TestInitialize]
        public void Init()
        {
            var tracateSql = @"TRUNCATE TABLE t_wallet";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Execute(tracateSql);
            }

            repo = new WalletRepository(connStr);
        }

        [TestMethod]
        public void Insert()
        {
            var insertResult = this.repo.Insert(new WalletPo());
            Assert.IsNull(insertResult.exception);
            Assert.AreEqual(insertResult.walletPo.f_id, 1);
        }

        [TestMethod]
        public void AddBalance()
        {
            var insertResult = this.repo.Insert(new WalletPo());
            Assert.IsNull(insertResult.exception);
            var wallet = insertResult.walletPo;
            var addResult = this.repo.AddBalance(1, 100);
            Assert.IsNull(addResult.exception);
            Assert.AreEqual(addResult.opStatus, TxnStatus.Success);
            Assert.AreEqual(addResult.walletPo.f_balance, 100);
        }
        
        [TestMethod]
        public void AddBalance_Insufficient()
        {
            var insertResult = this.repo.Insert(new WalletPo());
            Assert.IsNull(insertResult.exception);
            var addResult = this.repo.AddBalance(1, -100);
            Assert.IsNull(addResult.exception);
            Assert.AreEqual(addResult.opStatus, TxnStatus.Insufficient);
            Assert.AreEqual(addResult.walletPo.f_balance, 0);
        }

        [TestMethod]
        public void Query()
        {
            var insertResult = this.repo.Insert(new WalletPo());
            Assert.IsNull(insertResult.exception);
            var wallet = insertResult.walletPo;
            var queryResult = this.repo.Query(1);
            Assert.IsNull(queryResult.exception);
            Assert.AreEqual(queryResult.walletPos.Count(), 1);
            Assert.IsNotNull(queryResult.walletPos.First());
        }
    }
}