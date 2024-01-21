using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
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
    public class LiquidationController : ControllerBase
    {
         ILoanService _ils;
         ILiquidate _liq;
        private readonly IBone _bankOne;

        public LiquidationController(ILoanService ils, ILiquidate ilq, IBone bankone)
        {
            _ils = ils;
            _liq = ilq;
            _bankOne = bankone;
        }
        [HttpGet]
        [Route("GetLoanHistory")]
        public async Task<IActionResult> GetCustomerNanoLoansAsync(string customerID)
        {
            return Ok(await _bankOne.GetCutomerLoanAccounts(customerID));
        }
        [HttpPost]
        [Route("RepayLoan")]
        public IActionResult RepayNanoloan(RepayLoanDTIO repaydto)
        {
            return Ok(_bankOne.RepaySofriNanoLoan(repaydto));
        }
        //[HttpPost]
        //[Route("FullLiquidation")]
        //public async Task<IActionResult> FullLoanLiquidation([FromBody]FullLoanLiquidationDTO fld)
        //{
        //    return Ok(await _liq.FullLiquidation(fld));
        //}
        //[HttpPost]
        //[Route("PartiarlLiquidation")]
        //public async Task<IActionResult> PartiaLiquidation([FromBody]PartialLoanLiquidationDTO pld)
        //{
        //    return Ok(await _liq.PartialLiquidation(pld));
        //}
    }
}
