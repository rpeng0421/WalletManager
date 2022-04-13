using System;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Entity
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public IWalletRepository WalletRepository;

        public Exception AddBalance(decimal amount)
        {
            if (Balance + amount < 0)
            {
                return new Exception("wallet balance amount insufficient");
            }

            var addBalanceResult = this.WalletRepository.AddBalance(amount);
            if (addBalanceResult.exception != null)
            {
                return addBalanceResult.exception;
            }

            Balance = addBalanceResult.walletPo.f_balance;
            return null;
        }
    }
}