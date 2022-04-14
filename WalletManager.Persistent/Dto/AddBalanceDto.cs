using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Persistent.Dto
{
    public class AddBalanceDto: WalletPo
    {
        public TxnStatus FOperationTxnStatus;
    }
}