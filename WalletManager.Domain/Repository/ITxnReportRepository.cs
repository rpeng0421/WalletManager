using System;
using System.Collections.Generic;
using WalletManager.Domain.Model.DepositReport;

namespace WalletManager.Domain.Repository
{
    public interface ITxnReportRepository
    {
        (Exception exception, WalletTxnReport txnReport) Upsert(TxnReport txnReport);
        (Exception exception, IEnumerable<WalletTxnReport> txnReports) Query();
        (Exception exception, WalletTxnReport txnReport) Get(int walletId);
    }
}