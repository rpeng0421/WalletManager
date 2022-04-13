using System;
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
            var preBalance = Wallet.Balance;
            Wallet.AddBalance(amount);
            var afterBalance = Wallet.Balance;
            var insertResult = WalletTxnRepository.Insert(new WalletTxnPo
            {
                f_walletId = this.Wallet.Id,
                f_preBalance = preBalance,
                f_amount = amount,
                f_afterBalance = afterBalance
            });
            if (insertResult.exception != null)
            {
                return (insertResult.exception, null);
            }

            return (null, insertResult.walletTxnPo.ToDomain());
        }
    }
}