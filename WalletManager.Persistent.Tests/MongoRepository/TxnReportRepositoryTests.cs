using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using WalletManager.Domain.Model.DepositReport;
using WalletManager.Domain.Repository;
using WalletManager.Persistent.MongoRepository;

namespace WalletManager.Persistent.Tests.MongoRepository
{
    [TestClass]
    public class TxnReportRepositoryTests
    {
        private const string mongoConn = @"mongodb://localhost:27017";
        private ITxnReportRepository repo;

        [TestInitialize]
        public void Init()
        {
            var client = new MongoClient(mongoConn);
            client
                .GetDatabase(TxnReportRepository.DbName)
                .DropCollection(TxnReportRepository.Collection);

            this.repo = new TxnReportRepository(client);
        }

        [TestMethod]
        public void Upsert()
        {
            var upsertResult = this.repo.Upsert(new TxnReport
            {
                WalletId = 1,
                TxnType = TxnReport.DepositType,
                Amount = 100,
                Count = 1
            });

            Assert.IsNull(upsertResult.exception);
            Assert.AreEqual(upsertResult.txnReport.TxnReportDict[TxnReport.DepositType].Amount, 100);
            
            upsertResult = this.repo.Upsert(new TxnReport
            {
                WalletId = 1,
                TxnType = TxnReport.WithdrawType,
                Amount = 100,
                Count = 1
            });
            
            Assert.IsNull(upsertResult.exception);
            Assert.AreEqual(upsertResult.txnReport.TxnReportDict[TxnReport.WithdrawType].Amount, 100);
        }
        
        [TestMethod]
        public void Get()
        {
            var upsertResult = this.repo.Upsert(new TxnReport
            {
                WalletId = 1,
                TxnType = TxnReport.DepositType,
                Amount = 100,
                Count = 1
            });

            Assert.IsNull(upsertResult.exception);

            var getResult = this.repo.Get(1);
            
            Assert.IsNull(getResult.exception);
            Assert.AreEqual(getResult.txnReport.TxnReportDict[TxnReport.DepositType].Amount, 100);
        }        
        
        [TestMethod]
        public void Query()
        {
            var upsertResult = this.repo.Upsert(new TxnReport
            {
                WalletId = 1,
                TxnType = TxnReport.DepositType,
                Amount = 100,
                Count = 1
            });

            Assert.IsNull(upsertResult.exception);

            var getResult = this.repo.Query();
            
            Assert.IsNull(getResult.exception);
            Assert.AreEqual(getResult.txnReports.Count(), 1);
        }
    }
}