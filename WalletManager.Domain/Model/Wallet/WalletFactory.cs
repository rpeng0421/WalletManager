using System;
using System.Linq;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model.Wallet
{
    public class WalletFactory
    {
        public WalletFactory(
            IWalletRepository walletRepository,
            IWalletTxnRepository walletTxnRepository
        )
        {
            WalletRepository = walletRepository;
            WalletTxnRepository = walletTxnRepository;
        }

        public IWalletRepository WalletRepository { get; set; }
        public IWalletTxnRepository WalletTxnRepository { get; set; }

        /// <summary>
        ///     創建新錢包
        /// </summary>
        /// <returns></returns>
        public (Exception exception, WalletAggregate walletAggregate) CreateWallet()
        {
            var insertResult = WalletRepository.Insert(new WalletPo
            {
                f_balance = 0
            });
            if (insertResult.exception != null) return (insertResult.exception, null);

            var walletPo = insertResult.walletPo;
            return (null, new WalletAggregate
            {
                Wallet = new Wallet
                {
                    Id = walletPo.f_id,
                    Balance = walletPo.f_balance,
                    WalletRepository = WalletRepository
                },
                WalletRepository = WalletRepository,
                WalletTxnRepository = WalletTxnRepository
            });
        }

        /// <summary>
        ///     取得錢包 aggregate by ID
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        public (Exception exception, WalletAggregate walletAggregate) Resolve(int walletId)
        {
            var queryResult = WalletRepository.Query(walletId);
            if (queryResult.exception != null) return (queryResult.exception, null);
            if (!queryResult.walletPos.Any())
            {
                return (null, null);
            }

            var walletPo = queryResult.walletPos.First();
            return (null, new WalletAggregate
            {
                Wallet = new Wallet
                {
                    Id = walletPo.f_id,
                    Balance = walletPo.f_balance,
                    WalletRepository = WalletRepository
                },
                WalletRepository = WalletRepository,
                WalletTxnRepository = WalletTxnRepository
            });
        }
    }
}