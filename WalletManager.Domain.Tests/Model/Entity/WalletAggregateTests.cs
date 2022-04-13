using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletManager.Domain.Model.Entity;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Tests.Model.Entity
{
    [TestClass]
    public class WalletAggregateTests
    {
        [TestMethod]
        public void AddBalance()
        {
            var walletRepo = new Mock<IWalletRepository>();
            var walletTxnRepo = new Mock<IWalletTxnRepository>();
            var walletAgg = new WalletAggregate
            {
                Wallet = new Wallet
                {
                    Id = 1,
                    Balance = 100,
                    WalletRepository = walletRepo.Object
                },
                WalletRepository = walletRepo.Object,
                WalletTxnRepository = walletTxnRepo.Object
            };
            walletRepo.Setup(p => p.AddBalance(It.IsAny<decimal>()))
                .Returns((null, new WalletPo
                {
                    f_id = 1,
                    f_balance = 200
                }));

            walletTxnRepo.Setup(p => p.Insert(It.IsAny<WalletTxnPo>()))
                .Returns((null, new WalletTxnPo
                {
                    f_id = 1,
                    f_walletId = 1,
                    f_preBalance = 100,
                    f_amount = 100,
                    f_afterBalance = 200,
                    f_createdAt = DateTime.Now
                }));
            
            var addBalanceResult = walletAgg.AddBalance(100);
            Assert.IsNull(addBalanceResult.exception);
            Assert.AreEqual(addBalanceResult.walletTxn.Amount, 100);
            Assert.AreEqual(addBalanceResult.walletTxn.AfterBalance, 200);
        }
    }
}