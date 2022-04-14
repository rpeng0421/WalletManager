using System;
using System.Collections.Generic;
using System.Linq;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Wallet
{
    public class WalletAggregate
    {
        public Wallet Wallet;
        public IWalletRepository WalletRepository;
        public IWalletTxnRepository WalletTxnRepository;

        public (Exception exception, TxnResultDto walletTxnResult) AddBalance(decimal amount)
        {
            var walletAddBalanceResult = Wallet.AddBalance(amount);
            if (walletAddBalanceResult.exception != null) return (walletAddBalanceResult.exception, null);

            if (walletAddBalanceResult.opStatus != TxnStatus.Success)
                return (null, new TxnResultDto
                {
                    Wallet = Wallet,
                    WalletTxn = null,
                    TxnStatus = walletAddBalanceResult.opStatus
                });

            var insertResult = WalletTxnRepository.Insert(new List<WalletTxnPo>
            {
                new WalletTxnPo
                {
                    f_walletId = Wallet.Id,
                    f_amount = amount,
                    f_balance = Wallet.Balance,
                    f_createdAt = DateTime.Now
                }
            });
            if (insertResult.exception != null) return (insertResult.exception, null);

            return (null, new TxnResultDto
            {
                Wallet = Wallet,
                WalletTxn = insertResult.walletTxnPos.First().ToDomain(),
                TxnStatus = walletAddBalanceResult.opStatus
            });
        }
    }
}