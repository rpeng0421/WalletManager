using System;
using System.Collections.Generic;
using WalletManager.Domain.Model.DepositReport;

namespace WalletManager.Domain.Repository
{
    public interface ITxnReportRepository
    {
        (Exception exception, TxnReport txnReport) Upsert(TxnReport txnReport);
        (Exception exception, IEnumerable<TxnReport> txnReports) Query(int? walletId);
    }
}