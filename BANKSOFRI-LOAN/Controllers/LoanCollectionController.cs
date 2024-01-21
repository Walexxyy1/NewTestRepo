using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCollectionController : ControllerBase
    {
        private readonly IOverDue odc;
        private readonly ICollect cs;
        private readonly ILoanApp la;
        private readonly IRecurringJobManager _irm;

        public LoanCollectionController(IOverDue _odc, ICollect _cs, ILoanApp _la, IRecurringJobManager irm)
        {
            odc = _odc;
            cs = _cs;
            la = _la;
            _irm = irm;
        }
        [HttpGet]
        [Route("RetryFailedDisbursements")]
        public IActionResult RetryLoanDisbursement()
        {

           // RecurringJob.AddOrUpdate("MyJob", () => la.RetryNanoLoanDisbursement(), Cron.MinuteInterval);
            return Ok();
        }
        [HttpGet]
        [Route("StartLoanCollection")]
        public async Task<IActionResult> StarLoanCollection()
        {
            await odc.StartOverDueCollection();
            await cs.StartRepaymentCollection();
            return Ok();
        }
    }
}
