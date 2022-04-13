using System;
using System.Collections.Generic;
using System.Linq;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Entity
{
    public class WalletAggregate
    {
        public Wallet Wallet;
        public IWalletRepository WalletRepository;
        public IWalletTxnRepository WalletTxnRepository;

        public (Exception exception, WalletTxn walletTxn) AddBalance(decimal amount)
        {
            Wallet.AddBalance(amount);
            var insertResult = WalletTxnRepository.Insert(new List<WalletTxnPo>()
            {
                new WalletTxnPo
                {
                    f_walletId = this.Wallet.Id,
                    f_amount = amount,
                    f_balance = Wallet.Balance
                }
            });
            if (insertResult.exception != null)
            {
                return (insertResult.exception, null);
            }

            return (null, insertResult.walletTxnPos.First().ToDomain());
        }
    }
}