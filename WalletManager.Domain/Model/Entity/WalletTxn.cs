using System;

namespace WalletManager.Domain.Model.Entity
{
    public class WalletTxn
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal PreBalance { get; set; }
        public decimal Amount { get; set; }
        public decimal AfterBalance { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}