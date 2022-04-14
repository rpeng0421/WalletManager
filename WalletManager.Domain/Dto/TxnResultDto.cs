using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Domain.Dto
{
    public class TxnResultDto
    {
        public Wallet Wallet { get; set; }
        public WalletTxn WalletTxn { get; set; }
        public TxnStatus TxnStatus { get; set; }
    }
}