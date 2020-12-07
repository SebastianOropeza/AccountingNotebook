using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingNotebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly InMemoryAccount financialAccount;

        public DefaultController(InMemoryAccount financialAccount)
        {
            this.financialAccount = financialAccount;
        }

        [HttpGet]
        public ActionResult GetAccountBalance()
        {
            var balance = financialAccount.Account.Balance;
            return Ok(balance);
        }
    }
}
