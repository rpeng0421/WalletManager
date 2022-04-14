using System;
using WalletManager.Ap.Model;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class CreateWalletAp : IApplication
    {
        private readonly WalletFactory walletFactory;

        public CreateWalletAp(WalletFactory walletFactory)
        {
            this.walletFactory = walletFactory;
        }

        public (Exception exception, Wallet wallet) Execute()
        {
            try
            {
                Logger.Debug("Create wallet start");
                var createResult = walletFactory.CreateWallet();
                if (createResult.exception != null) throw createResult.exception;

                return (null, createResult.walletAggregate.Wallet);
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}