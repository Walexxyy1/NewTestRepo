using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherLoansController : ControllerBase
    {
        ILoanApp _lna;
        ILogs dblog;
        IOthers _o;
        public OtherLoansController(ILoanApp lna, IOthers o, ILogs log)
        {
            _lna = lna;
            dblog = log;
            _o = o;
        }

        [HttpPost]
        [Route("OtherLoans")]
        public async Task<IActionResult> CreateOtherLoan([FromBody] OtherLoansDTO ol)
        {
            ResponseObject response = new ResponseObject();
            try
            {
                dblog.Logger("Create new Other Loan Request received with the following parameters" + JsonConvert.SerializeObject(ol), "INFO");
                response = await _o.SaveOtherLoanApplication(ol);
            }
            catch (Exception ex)
            {
                dblog.Logger("An error occurred on CreateOtherLoan endpoint. Details: " + ex.Message, "Error");
            }
            return Ok(response);
        }
        [HttpGet]
        [Route("FetchOtherLoans")]
        public IActionResult GetAllOtherLoans()
        {
            return Ok(_o.FetchOtherLoans());
        }

        [HttpGet]
        [Route("FetchCustomerOtherLoans")]
        public IActionResult GetCustomerOtherLoans(string customerId)
        {
            return Ok(_o.FetchCustomerOtherLoans(customerId));
        }
    }
}
