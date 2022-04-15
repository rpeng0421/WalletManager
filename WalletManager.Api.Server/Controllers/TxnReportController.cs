using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using NLog;
using WalletManager.Api.Server.Dto;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Repository;

namespace WalletManager.Api.Server.Controllers
{
    [RoutePrefix("api/txnReport")]
    public class TxnReportController : ApiController
    {
        private readonly ITxnReportRepository txnReportRepository;
        private readonly ILogger logger = LogManager.GetLogger("WalletManager.Api.Server");

        public TxnReportController(ITxnReportRepository txnReportRepository)
        {
            this.txnReportRepository = txnReportRepository;
        }

        [HttpGet]
        public HttpResponseMessage Query()
        {
            try
            {
                var queryResult = this.txnReportRepository.Query();
                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(queryResult.txnReports));
                return rs;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage Query(int walletId)
        {
            try
            {
                var queryResult = this.txnReportRepository.Get(walletId);
                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(queryResult.txnReport));
                return rs;
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}