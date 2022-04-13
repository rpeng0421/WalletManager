using System;
using System.Collections.Generic;
using System.Linq;
using WalletManager.Domain.Model.Po;
using WalletManager.Domain.Model.Wallet;
using WalletManager.Domain.Repository;

namespace WalletManager.Domain.Model
{
    public class WalletFactory
    {
        public IWalletRepository WalletRepository { get; set; }
        public IWalletTxnRepository WalletTxnRepository { get; set; }

        public WalletFactory(
            IWalletRepository walletRepository,
            IWalletTxnRepository walletTxnRepository
        )
        {
            WalletRepository = walletRepository;
            WalletTxnRepository = walletTxnRepository;
        }

        /// <summary>
        /// 創建新錢包
        /// </summary>
        /// <returns></returns>
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
                Wallet = new Wallet.Wallet
                {
                    Id = walletPo.f_id,
                    Balance = walletPo.f_balance,
                    WalletRepository = this.WalletRepository
                },
                WalletRepository = this.WalletRepository,
                WalletTxnRepository = this.WalletTxnRepository
            });
        }

        /// <summary>
        /// 取得錢包 aggregate by ID
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        public (Exception exception, WalletAggregate walletAggregate) Resolve(int walletId)
        {
            var queryResult = this.WalletRepository.Query(walletId);
            if (queryResult.exception != null)
            {
                return (queryResult.exception, null);
            }

            var walletPo = queryResult.walletPos.First();
            return (null, new WalletAggregate
            {
                Wallet = new Wallet.Wallet
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