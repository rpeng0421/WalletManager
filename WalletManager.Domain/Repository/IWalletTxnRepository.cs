using System;
using WalletManager.Domain.Model.Po;

namespace WalletManager.Domain.Repository
{
    public interface IWalletTxnRepository
    {
        (Exception exception, WalletTxnPo walletTxnPo) Insert(WalletTxnPo walletTxnPo);
    }
}