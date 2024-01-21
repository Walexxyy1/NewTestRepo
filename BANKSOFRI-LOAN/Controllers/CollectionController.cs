using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.Collection;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IOverDue odc;
        private readonly ICollect cs;
        private readonly ILoanApp la;
        private readonly IRecurringJobManager _irm;
        private readonly ILoanService _ls;

        public CollectionController(IOverDue _odc, ICollect _cs, ILoanApp _la, IRecurringJobManager irm, ILoanService ls)
        {
            odc = _odc;
            cs = _cs;
            la = _la;
            _irm = irm;
            _ls = ls;
        }

        [HttpGet]
        [Route("testfailedtransactions")]
        public IActionResult getfailedrepay()
        {
            var reply = _ls.GetAllUnpaidRepaymentDue();
            return Ok(reply);
        }

        [HttpGet]
        [Route("RetryFailedDisbursements")]
        public IActionResult RetryLoanDisbursement()
        {
          //  RecurringJob.AddOrUpdate("SOFRI",() => la.RetryNanoLoanDisbursement(), Cron.Minutely);
            return Ok();
        }
        [HttpGet]
        [Route("StartLoanCollection")]
        public async Task<IActionResult> StarLoanCollection()
        {            
            //await odc.StartOverDueCollection();
            List<CollectionDTO> loans = await cs.GetRecords();
            return Ok(new {loans = loans, total = loans.Count() });
        }
    }
}
