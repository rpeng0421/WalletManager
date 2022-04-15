using System;
using System.Collections.Generic;

namespace WalletManager.Domain.Model.DepositReport
{
    public class WalletTxnReport
    {
        public int WalletId { get; set; }
        public Dictionary<string, TxnReport> TxnReportDict { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}