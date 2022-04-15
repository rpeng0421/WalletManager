using System;
using Newtonsoft.Json;
using NLog;
using WalletManager.Domain.Event;
using WalletManager.Domain.Repository;
using WalletManager.RabbitMq.Model;

namespace WalletManager.Ap.Applibs
{
    public class BalanceChangeConsumer : IConsumerHandler<EventData>
    {
        private ILogger logger = LogManager.GetLogger("WalletManager.Ap.Applibs");
        private readonly ITxnCounterRepository counterRepository;
        private readonly ITxnReportRepository txnReportRepository;

        public BalanceChangeConsumer(ITxnCounterRepository counterRepository, ITxnReportRepository txnReportRepository)
        {
            this.counterRepository = counterRepository;
            this.txnReportRepository = txnReportRepository;
        }

        public bool Handle(EventData eventData)
        {
            try
            {
                var data = JsonConvert
                    .DeserializeObject<BalanceChangeEvent>(eventData.Data);
                var addTxnResult = this.counterRepository.AddTxn(data.WalletId, data.TxnType, data.Amount);
                if (addTxnResult.exception != null)
                {
                    throw addTxnResult.exception;
                }

                this.logger.Debug($"add txn count result {JsonConvert.SerializeObject(addTxnResult.txnReport)}");

                var updateReportResult = this.txnReportRepository.Upsert(addTxnResult.txnReport);
                if (updateReportResult.exception != null)
                {
                    throw updateReportResult.exception;
                }

                this.logger.Debug(
                    $"update txn report success, {JsonConvert.SerializeObject(updateReportResult.txnReport)}");
                return true;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{this.GetType().FullName} process fail");
                return false;
            }
        }
    }
}