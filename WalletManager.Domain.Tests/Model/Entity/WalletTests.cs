using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Tests.Model.Entity
{
    [TestClass]
    public class WalletTests
    {
        [TestMethod]
        public void AddBalance()
        {
            var walletRepo = new Mock<IWalletRepository>();
            var wallet = new Wallet
            {
                Id = 1,
                Balance = 100,
                WalletRepository = walletRepo.Object
            };
            walletRepo.Setup(p => p.AddBalance(It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns((null, new WalletPo
                {
                    f_id = 1,
                    f_balance = 120
                }, TxnStatus.Success));
            var addWalletResult = wallet.AddBalance(20);
            Assert.IsNull(addWalletResult.exception);
            Assert.AreEqual(wallet.Balance, 120);
        }
    }
}