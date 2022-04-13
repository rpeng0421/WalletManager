using System;
using WalletManager.Domain.Model;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class CreateWalletAp
    {
        private WalletFactory walletFactory;

        public CreateWalletAp(WalletFactory walletFactory)
        {
            this.walletFactory = walletFactory;
        }

        public (Exception exception, Wallet wallet) Execute()
        {
            try
            {
                var createResult = this.walletFactory.CreateWallet();
                if (createResult.exception != null)
                {
                    throw createResult.exception;
                }

                return (null, createResult.walletAggregate.Wallet);
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}