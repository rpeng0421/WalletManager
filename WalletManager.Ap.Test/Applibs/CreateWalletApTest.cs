using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NLog;
using WalletManager.Ap.Applibs;
using WalletManager.Ap.Model;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Ap.Test.Applibs
{
    [TestClass]
    public class CreateWalletApTest
    {
        [TestMethod]
        public void CreateWallet()
        {
            var walletRepo = new Mock<IWalletRepository>();
            var walletTxnRepo = new Mock<IWalletTxnRepository>();
            walletRepo.Setup(p => p.Insert(It.IsAny<WalletPo>()))
                .Returns((null, new WalletPo
                {
                    f_id = 2,
                    f_balance = 100
                }));
            var createWalletAp = new CreateWalletAp(new WalletFactory(walletRepo.Object, walletTxnRepo.Object))
            {
                Logger = LogManager.GetLogger("test")
            };

            var createResult = createWalletAp.Execute();
            Assert.IsNull(createResult.exception);
            Assert.AreEqual(createResult.wallet.Id, 2);
            Assert.AreEqual(createResult.wallet.Balance, 100);
        }
    }
}