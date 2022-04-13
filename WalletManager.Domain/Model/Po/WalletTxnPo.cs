using System;
using WalletManager.Domain.Model.Entity;

namespace WalletManager.Domain.Model.Po
{
    public class WalletTxnPo
    {
        public int f_id { get; set; }

        public int f_walletId { get; set; }

        public decimal f_preBalance { get; set; }

        public decimal f_amount { get; set; }

        public decimal f_afterBalance { get; set; }

        public DateTime f_createdAt { get; set; }

        public WalletTxn ToDomain()
        {
            return new WalletTxn
            {
                Id = f_id,
                WalletId = f_walletId,
                PreBalance = f_preBalance,
                Amount = f_amount,
                AfterBalance = f_afterBalance,
                CreatedAt = f_createdAt
            };
        }
    }
}