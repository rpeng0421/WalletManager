using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WalletManager.Domain.Model;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Tests.Model
{
    [TestClass]
    public class WalletFactoryTests
    {
        [TestMethod]
        public void CreateWallet()
        {
            var walletRepo = new Mock<IWalletRepository>();
            var walletTxnRepo = new Mock<IWalletTxnRepository>();
            var walletFactory = new WalletFactory(walletRepo.Object, walletTxnRepo.Object);
            walletRepo.Setup(p => p.Insert(It.IsAny<WalletPo>()))
                .Returns((null, new WalletPo
                {
                    f_id = 1,
                    f_balance = 0
                }));
            var createWalletResult = walletFactory.CreateWallet();
            Assert.IsNull(createWalletResult.exception);
            Assert.AreEqual(createWalletResult.walletAggregate.Wallet.Id, 1);
        }

        [TestMethod]
        public void ResolveWallet()
        {
            var walletRepo = new Mock<IWalletRepository>();
            var walletTxnRepo = new Mock<IWalletTxnRepository>();
            var walletFactory = new WalletFactory(walletRepo.Object, walletTxnRepo.Object);
            walletRepo.Setup(p => p.Query(It.IsAny<int>()))
                .Returns((null, new List<WalletPo>()
                {
                    new WalletPo
                    {
                        f_id = 1,
                        f_balance = 100
                    }
                }));

            var resolveResult = walletFactory.Resolve(1);
            Assert.IsNull(resolveResult.exception);
            Assert.AreEqual(resolveResult.walletAggregate.Wallet.Balance, 100);
        }
    }
}