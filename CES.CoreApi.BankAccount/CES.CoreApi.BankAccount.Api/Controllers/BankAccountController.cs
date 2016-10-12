using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Services;
using CES.CoreApi.BankAccount.Api.ViewModels;
using System.Net;
using System.Web.Http;


namespace CES.CoreApi.BankAccount.Api.Controllers
{
    [RoutePrefix("Service")]
    public class BankAccountController : ApiController
    {
        private BankAccountService _service;
        public BankAccountController()
        {
            _service = new BankAccountService();
        }
        [HttpGet]
        [Route("BankDepositValidation")]
        public IHttpActionResult BankDepositValidation([FromUri] ServiceBankDepositRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bankDepositRequest =  AutoMapper.Mapper.Map<ValidateBankDepositRequest>(request);

            var response = _service.ValidateBankDeposit(bankDepositRequest);
            return Content(HttpStatusCode.OK, response);
        }
    }
}
