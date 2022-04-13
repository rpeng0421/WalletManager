using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Entity
{
    public class WalletAggregate
    {
        public Wallet Wallet;
        public IWalletRepository WalletRepository;
        public IWalletTxnRepository WalletTxnRepository;

        public WalletAggregate()
        {
        }

        public void AddBalance()
        {
            
        }
    }
}