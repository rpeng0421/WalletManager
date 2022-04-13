using System;
using System.Collections.Generic;
using WalletManager.Domain.Model.Po;

namespace WalletManager.Domain.Repository
{
    public interface IWalletTxnRepository
    {
        /// <summary>
        /// 新增錢包交易結果
        /// </summary>
        (Exception exception, IEnumerable<WalletTxnPo> walletTxnPos) Insert(IEnumerable<WalletTxnPo> walletTxnPo);
        /// <summary>
        /// 查詢錢包交易
        /// </summary>
        /// <param name="walletId"></param>
        /// <returns></returns>
        (Exception exception, IEnumerable<WalletTxnPo> walletTxnPos) Query(int? walletId);
    }
}