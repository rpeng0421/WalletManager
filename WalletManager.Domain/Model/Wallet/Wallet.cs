using System;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Wallet
{
    public class Wallet
    {
        /// <summary>
        /// 錢包ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 錢包餘額
        /// </summary>
        public decimal Balance { get; set; }

        public IWalletRepository WalletRepository;

        public Exception AddBalance(decimal amount)
        {
            if (Balance + amount < 0)
            {
                return new Exception("wallet balance amount insufficient");
            }

            var addBalanceResult = this.WalletRepository.AddBalance(Id, amount);
            if (addBalanceResult.exception != null)
            {
                return addBalanceResult.exception;
            }

            Balance = addBalanceResult.walletPo.f_balance;
            return null;
        }
    }
}