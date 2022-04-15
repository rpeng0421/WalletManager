using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WalletManager.Domain.Model.DepositReport;
using WalletManager.Domain.Repository;

namespace WalletManager.Persistent.MongoRepository
{
    public class TxnReportRepository : ITxnReportRepository
    {
        public const string DbName = "WalletManager";
        public const string Collection = "WalletTxnReport";
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<WalletTxnReport> collection;

        static TxnReportRepository()
        {
            BsonClassMap.RegisterClassMap<WalletTxnReport>(
                cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapIdMember(c => c.WalletId);
                });
        }

        public TxnReportRepository(MongoClient client)
        {
            this.client = client;
            this.database = client.GetDatabase(DbName);
            this.collection = database.GetCollection<WalletTxnReport>(Collection);
            this.collection.Indexes.CreateMany(new CreateIndexModel<WalletTxnReport>[]
            {
                new CreateIndexModel<WalletTxnReport>(Builders<WalletTxnReport>.IndexKeys.Ascending(p => p.WalletId)),
                new CreateIndexModel<WalletTxnReport>(
                    Builders<WalletTxnReport>.IndexKeys.Descending(p => p.UpdateDateTime)),
            });
        }

        public (Exception exception, WalletTxnReport txnReport) Upsert(TxnReport txnReport)
        {
            var filter = Builders<WalletTxnReport>.Filter.Eq(p => p.WalletId, txnReport.WalletId);
            var updater = Builders<WalletTxnReport>.Update
                .Set(p => p.TxnReportDict[txnReport.TxnType], txnReport)
                .Set(p => p.UpdateDateTime, DateTime.Now);
            this.collection.UpdateOne(filter, updater, new UpdateOptions() {IsUpsert = true});

            return Get(txnReport.WalletId);
        }

        public (Exception exception, IEnumerable<WalletTxnReport> txnReports) Query()
        {
            var wallets = this.collection.Find(Builders<WalletTxnReport>.Filter.Empty).ToList();
            return (null, wallets);
        }

        public (Exception exception, WalletTxnReport txnReport) Get(int walletId)
        {
            var wallet = this.collection.Find(p => p.WalletId == walletId).FirstOrDefault();
            if (wallet == null)
            {
                return (new Exception("mongo not exist record"), null);
            }

            return (null, wallet);
        }
    }
}