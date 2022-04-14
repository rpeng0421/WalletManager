namespace WalletManager.Domain.Model.DepositReport
{
    public class TxnReport
    {
        public int WalletId { get; set; }
        public string TxnType { get; set; }
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}