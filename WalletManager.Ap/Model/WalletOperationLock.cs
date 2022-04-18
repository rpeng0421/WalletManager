using RedLockNet;
using WalletManager.Ap.NoSqlService;

namespace WalletManager.Ap.Model
{
    public class WalletOperationLock
    {
        public int WalletId;

        public readonly string LockKey;

        public WalletOperationLock(int walletId)
        {
            WalletId = walletId;
            LockKey = $"WalletOperationLock.WalletId.{WalletId}";
        }

        public IRedLock GrabLock()
        {
            return RedisLockFactory.GetLock(LockKey);
        }
    }
}