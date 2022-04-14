using System;
using WalletManager.Domain.Model.DepositReport;

namespace WalletManager.Domain.Repository
{
    public interface ITxnCounterRepository
    {
        (Exception exception, TxnReport txnReport ) AddTxn(int walletId, string countType, decimal amount);
    }
}