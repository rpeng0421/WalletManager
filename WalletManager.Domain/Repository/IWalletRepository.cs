using System;
using WalletManager.Domain.Model.Entity;
using WalletManager.Domain.Model.Po;

namespace WalletManager.Domain.Repository
{
    public interface IWalletRepository
    {
        (Exception exception, WalletPo walletPo) Insert(WalletPo wallet);
        (Exception exception, WalletPo walletPo) AddBalance(decimal amount);
        (Exception exception, WalletPo walletPo) Query(int? walletId);
    }
}