using System;
using WalletManager.Ap.Event;
using WalletManager.Ap.Model;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Event;
using WalletManager.Domain.Model.DepositReport;
using WalletManager.Domain.Model.Wallet;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Ap.Applibs
{
    public class WithdrawAp : IApplication
    {
        private readonly WalletFactory walletFactory;
        private readonly RabbitMqFactory rabbitMqFactory;
        private readonly BalanceChangePublisher publisher;

        public WithdrawAp(
            WalletFactory walletFactory,
            RabbitMqFactory rabbitMqFactory,
            BalanceChangePublisher publisher
        )
        {
            this.walletFactory = walletFactory;
            this.rabbitMqFactory = rabbitMqFactory;
            this.publisher = publisher;
        }

        public (Exception exception, TxnResultDto txnResult) Execute(int walletId, decimal amount)
        {
            if (amount < 0) return (null, new TxnResultDto {TxnStatus = TxnStatus.IllegalAmount});

            try
            {
                TxnResultDto walletTxnResult = null;
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
                        if (addResult.txnResultDto.TxnStatus != TxnStatus.Success)
                        {
                            return (null, addResult.txnResultDto);
                        }

                        walletTxnResult = addResult.txnResultDto;
                    }
                }

                if (walletTxnResult == null)
                {
                    throw new Exception($"{nameof(GetType)} get wallet lock fail");
                }

                this.publisher.Publish(new BalanceChangeEvent
                {
                    WalletId = walletTxnResult.WalletTxn.WalletId,
                    TxnType = TxnReport.WithdrawType,
                    Amount = walletTxnResult.WalletTxn.Amount,
                    Count = 1
                });
                return (null, walletTxnResult);
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}