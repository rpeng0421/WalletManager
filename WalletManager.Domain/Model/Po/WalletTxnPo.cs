using System;

namespace WalletManager.Domain.Model.Po
{
    public class WalletTxnPo
    {
        public int f_id { get; set; }

        public int f_walletId { get; set; }

        public decimal f_preBalance { get; set; }

        public decimal f_changeAmount { get; set; }

        public decimal f_afterBalance { get; set; }

        public DateTime f_createdAt { get; set; }
    }
}