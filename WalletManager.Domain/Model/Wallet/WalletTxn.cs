using System;

namespace WalletManager.Domain.Model.Wallet
{
    public class WalletTxn
    {
        /// <summary>
        /// 交易ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 錢包ID
        /// </summary>
        public int WalletId { get; set; }
        /// <summary>
        /// 變動金額
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 變動後金額
        /// </summary>
        public decimal Balance { get; set; }
        
        /// <summary>
        /// 交易建立時間
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}