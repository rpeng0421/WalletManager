using System;
using WalletManager.Domain.Model.Entity;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model
{
    public class WalletFactory
    {
        public IWalletRepository WalletRepository;
        public IWalletTxnRepository WalletTxnRepository;

        public WalletFactory(
            IWalletRepository walletRepository,
            IWalletTxnRepository walletTxnRepository
        )
        {
            WalletRepository = walletRepository;
            WalletTxnRepository = walletTxnRepository;
        }

        public (Exception exception, WalletAggregate walletAggregate) CreateWallet()
        {
            var insertResult = this.WalletRepository.Insert(new WalletPo()
            {
                f_balance = 0
            });
            if (insertResult.exception != null)
            {
                return (insertResult.exception, null);
            }

            var walletPo = insertResult.walletPo;
            return (null, new WalletAggregate
            {
                Wallet = new Wallet
                {
                    Id = walletPo.f_id,
                    Balance = walletPo.f_balance,
                    WalletRepository = this.WalletRepository
                },
                WalletRepository = this.WalletRepository,
                WalletTxnRepository = this.WalletTxnRepository
            });
        }

        public (Exception exception, WalletAggregate walletAggregate) Resolve(int walletId)
        {
            var queryResult = this.WalletRepository.Query(walletId);
            if (queryResult.exception != null)
            {
                return (queryResult.exception, null);
            }

            var walletPo = queryResult.walletPo;
            return (null, new WalletAggregate
            {
                Wallet = new Wallet
                {
                    Id = walletPo.f_id,
                    Balance = walletPo.f_balance,
                    WalletRepository = this.WalletRepository
                },
                WalletRepository = this.WalletRepository,
                WalletTxnRepository = this.WalletTxnRepository
            });
        }
    }
}