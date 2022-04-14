using System;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Domain.Model.Po
{
    public class WalletTxnPo
    {
        /// <summary>
        ///     交易ID
        /// </summary>
        public int f_id { get; set; }

        /// <summary>
        ///     交易錢包ID
        /// </summary>
        public int f_walletId { get; set; }

        /// <summary>
        ///     變更金額
        /// </summary>
        public decimal f_amount { get; set; }

        /// <summary>
        ///     變更後餘額
        /// </summary>
        public decimal f_balance { get; set; }

        /// <summary>
        ///     交易建立時間
        /// </summary>
        public DateTime f_createdAt { get; set; }

        public WalletTxn ToDomain()
        {
            return new WalletTxn
            {
                Id = f_id,
                WalletId = f_walletId,
                Amount = f_amount,
                Balance = f_balance,
                CreatedAt = f_createdAt
            };
        }
    }
}