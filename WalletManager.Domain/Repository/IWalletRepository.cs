using System;
using WalletManager.Domain.Model.Entity;
using WalletManager.Domain.Model.Po;

namespace WalletManager.Domain.Repository
{
    public interface IWalletRepository
    {
        /// <summary>
        /// 新建一筆錢包紀錄
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        (Exception exception, WalletPo walletPo) Insert(WalletPo wallet);
        /// <summary>
        /// 調整錢包餘額
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        (Exception exception, WalletPo walletPo) AddBalance(decimal amount);
        /// <summary>
        /// 查詢錢包
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        (Exception exception, WalletPo walletPo) Query(int? walletId);
    }
}