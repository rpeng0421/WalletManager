namespace WalletManager.Domain.Model.DepositReport
{
    public class TxnReport
    {
        public static string DepositType = "Deposit";
        public static string WithdrawType = "Withdraw";
        public int WalletId { get; set; }
        public string TxnType { get; set; }
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}