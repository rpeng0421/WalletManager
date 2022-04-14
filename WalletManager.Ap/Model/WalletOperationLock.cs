﻿using RedLockNet;
using WalletManager.Ap.NosqlService;

namespace WalletManager.Ap.Model
{
    public class WalletOperationLock
    {
        public int WalletId;

        public WalletOperationLock(int walletId)
        {
            WalletId = walletId;
        }

        public IRedLock GrabLock()
        {
            return RedisLockFactory.GetLock($"WalletOperationLock.WalletId.{WalletId}");
        }
    }
}