namespace WalletManager.Domain.Model.Po
{
    public class WalletPo
    {
        /// <summary>
        /// 錢包ID
        /// </summary>
        public int f_id { get; set; }
        /// <summary>
        /// 錢包餘額
        /// </summary>
        public decimal f_balance { get; set; }
    }
}