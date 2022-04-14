using System;
using Newtonsoft.Json;
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

        [JsonIgnore] public IWalletRepository WalletRepository;

        public (Exception exception, TxnStatus opStatus) AddBalance(decimal amount)
        {
            var addBalanceResult = this.WalletRepository.AddBalance(Id, amount);
            if (addBalanceResult.exception != null)
            {
                return (addBalanceResult.exception, addBalanceResult.opStatus);
            }

            Balance = addBalanceResult.walletPo.f_balance;
            return (null, addBalanceResult.opStatus);
        }
    }
}