using System;
using System.Buffers;
using WalletManager.Ap.Dto;
using WalletManager.Ap.Model;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Model;
using WalletManager.Domain.Model.Wallet;

namespace WalletManager.Ap.Applibs
{
    public class WithdrawAp : IApplication
    {
        private WalletFactory walletFactory;

        public WithdrawAp(WalletFactory walletFactory)
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
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}