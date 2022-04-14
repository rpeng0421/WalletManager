using System;
using WalletManager.Ap.Model;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Model;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class DepositAp : IApplication
    {
        private WalletFactory walletFactory;

        public DepositAp(WalletFactory walletFactory)
        {
            this.walletFactory = walletFactory;
        }

        public (Exception exception, TxnResultDto txnResult) Execute(int walletId, decimal amount)
        {
            if (amount < 0)
            {
                return (null, new TxnResultDto() {TxnStatus = TxnStatus.IllegalAmount});
            }

            try
            {
                using (var rlock = new WalletOperationLock(walletId).GrabLock())
                {
                    if (rlock.IsAcquired)
                    {
                        var queryResult = this.walletFactory.Resolve(walletId);
                        if (queryResult.exception != null)
                        {
                            throw queryResult.exception;
                        }

                        var walletAgg = queryResult.walletAggregate;
                        var addResult = walletAgg.AddBalance(amount);
                        if (addResult.exception != null)
                        {
                            throw addResult.exception;
                        }

                        return (null, addResult.walletTxnResult);
                    }
                }

                throw new Exception($"{nameof(this.GetType)} get wallet lock fail");
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}