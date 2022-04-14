using System;
using NLog;
using WalletManager.Ap.Dto;
using WalletManager.Ap.Model;
using WalletManager.Domain.Model;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class CreateWalletAp: IApplication
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
                this.Logger.Debug("Create wallet start");
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