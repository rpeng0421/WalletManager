namespace WalletManager.Domain.Model.Entity
{
    public class WalletChangeLog
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal PreBalance { get; set; }
        public decimal ChangeAmount { get; set; }
        public decimal AfterBalance { get; set; }
    }
}