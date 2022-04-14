using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtonsoft.Json;
using NLog;
using WalletManager.Ap.Applibs;
using WalletManager.Api.Server.Applibs;
using WalletManager.Domain.Dto;
using WalletManager.Domain.Model;
using WalletManager.Domain.Repository;

namespace WalletManager.Api.Server.Controllers
{
    [RoutePrefix("api/wallet")]
    public class WalletController : ApiController
    {
        private readonly ILogger logger = LogManager.GetLogger("WalletManager.Api.Server");
        private readonly IWalletRepository walletRepository;
        private readonly WalletFactory walletFactory;

        public WalletController(
            IWalletRepository walletRepository, WalletFactory walletFactory)
        {
            this.walletRepository = walletRepository;
            this.walletFactory = walletFactory;
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri] QueryWalletDto queryWalletDto)
        {
            try
            {
                var queryResult = walletRepository.Query(queryWalletDto.WalletId);
                if (queryResult.exception != null) throw queryResult.exception;

                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(queryResult.walletPos));
                return rs;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("withdraw/{walletId}")]
        public HttpResponseMessage Withdraw([FromBody] AddBalanceDto param, int walletId)
        {
            try
            {
                var withdrawAp = AutofacConfig.Container.Resolve<WithdrawAp>();
                var withdrawResult = withdrawAp.Execute(walletId, param.Amount);
                if (withdrawResult.exception != null) throw withdrawResult.exception;

                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(withdrawResult.txnResult));
                return rs;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        [Route("deposit/{walletId}")]
        public HttpResponseMessage Deposit([FromBody] AddBalanceDto param, int walletId)
        {
            try
            {
                var depositAp = AutofacConfig.Container.Resolve<DepositAp>();
                var withdrawResult = depositAp.Execute(walletId, param.Amount);
                if (withdrawResult.exception != null) throw withdrawResult.exception;

                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(withdrawResult.txnResult));
                return rs;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage Create()
        {
            try
            {
                var createResult = AutofacConfig.Container.Resolve<CreateWalletAp>().Execute();
                if (createResult.exception != null) throw createResult.exception;

                var rs = new HttpResponseMessage(HttpStatusCode.OK);
                rs.Content = new StringContent(JsonConvert.SerializeObject(createResult.wallet));
                return rs;
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, $"{GetType().Name} Get Exception");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}