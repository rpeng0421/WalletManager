using System;
using WalletManager.Ap.Model;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class WithdrawAp : IApplication
    {
        private readonly WalletFactory walletFactory;

        public WithdrawAp(WalletFactory walletFactory)
        {
            this.walletFactory = walletFactory;
        }

        public (Exception exception, TxnResultDto txnResult) Execute(int walletId, decimal amount)
        {
            if (amount < 0) return (null, new TxnResultDto {TxnStatus = TxnStatus.IllegalAmount});

            try
            {
                using (var rlock = new WalletOperationLock(walletId).GrabLock())
                {
                    if (rlock.IsAcquired)
                    {
                        var queryResult = walletFactory.Resolve(walletId);
                        if (queryResult.exception != null)
                        {
                            if (queryResult.exception.Message.Equals("not exist wallet Id"))
                            {
                                return (null, new TxnResultDto()
                                {
                                    TxnStatus = TxnStatus.UnknownWallet
                                });
                            }

                            throw queryResult.exception;
                        }

                        var walletAgg = queryResult.walletAggregate;
                        var addResult = walletAgg.AddBalance(-amount);
                        if (addResult.exception != null) throw addResult.exception;

                        return (null, addResult.walletTxnResult);
                    }
                }

                throw new Exception($"{nameof(GetType)} get wallet lock fail");
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}