using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        ILoanApp _lna;
        ILogs dblog;
        ICrlogic cl;
        IOthers _o;
        public LoanController(ICrlogic _cl, ILoanApp lna, IOthers o, ILogs log)
        {

            _lna = lna;
            dblog = log;
            cl = _cl;
            _o = o;
        }

        [HttpPost]
        [Route("NanoLoanRequest")]
        public async Task<IActionResult> NanoLoanRequest([FromBody] NanoLoanRequestDTO pc)
        {
            LoanProccessingResponseObject response = new LoanProccessingResponseObject();
            try
            {
                dblog.Logger("Loan Request received with the following parameters" + JsonConvert.SerializeObject(pc), "INFO");
                response = await cl.ProccessCustomerLoanRequest(pc);
            }
            catch (Exception ex)
            {
                dblog.Logger("An error occurred on Customer Loan Request endpoint. Details: " + ex.Message, "Error");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateNanoLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] LoanRequestDTO lrd)
        {
            LoanRequestResponse response = new LoanRequestResponse();
            try
            {
                dblog.Logger("Create new Loan Request received with the following parameters" + JsonConvert.SerializeObject(lrd), "INFO");
                response = await _lna.ProcessNanoLoan(lrd);
            }
            catch (Exception ex)
            {
                dblog.Logger("An error occurred on CreateNanoLoan endpoint. Details: " + ex.Message, "Error");
            }
            return Ok(response);
        }

        //[HttpPost]
        //[Route("OtherLoans")]
        //public async Task<IActionResult> CreateOtherLoan([FromBody] OtherLoansDTO ol)
        //{
        //    ResponseObject response = new ResponseObject();
        //    try
        //    {
        //        dblog.Logger("Create new Other Loan Request received with the following parameters" + JsonConvert.SerializeObject(ol), "INFO");
        //        response = await _o.SaveOtherLoanApplication(ol);
        //    }
        //    catch (Exception ex)
        //    {
        //        dblog.Logger("An error occurred on CreateOtherLoan endpoint. Details: " + ex.Message, "Error");
        //    }
        //    return Ok(response);
        //}
    }
}
