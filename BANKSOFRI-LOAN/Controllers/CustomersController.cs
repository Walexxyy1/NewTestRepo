using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects;
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
    public class CustomersController : ControllerBase
    {
        ICustomer _cs;
        private readonly ILoanApp _lna;
        IBone _bi;
        ILoanService _ils;
       // private readonly IMoint _mo;

        public CustomersController(ICustomer cs, ILoanApp lna, IBone bi, ILoanService ils)
        {
            _cs = cs;
            _lna = lna;
            _bi = bi;
            _ils = ils;            
        }
        [HttpGet]
        [Route("CheckEliggibility")]
        public async Task<IActionResult> CheckCustomerEligibility(string customerId)
        {
            bool resp = await _cs.IsAccountValid(customerId);
            if (resp)
            {
                return Ok(new CreateCustomerResponse() { ResponseCode = "00", ResponseMessage = "Success! Account is valid" });
            }
            else
            {
                return Ok(new CreateCustomerResponse() { ResponseCode = "01", ResponseMessage = "Invalid Account! Account upgrade required" });
            }
        }
        [HttpPost]
        [Route("CreateNewCustomer")]
        public async Task<IActionResult> CreateNewCustomer([FromBody] CustomerDTO cd)
        {
            string resp = await _cs.CreateCustomer(cd);
            if (resp != null)
            {
                return Ok(new CreateCustomerResponse() { ResponseCode = "00", ResponseMessage = "Success", UserAccessDetails = resp });
            }
            else
            {
                return Ok(new CreateCustomerResponse() { ResponseCode = "01", ResponseMessage = "Failed Request" });
            }
        }             

        [HttpGet]
        [Route("GetCustomerLoanApplicationDetails")]
        public async Task<IActionResult> GetCustomerDetails(string customerID)
        {
            return Ok(await _cs.GetCustomerLoanProcessingDetailsFromDB(customerID));
        }

        [HttpGet]
        [Route("GetCustomerLoanOffers")]
        public async Task<IActionResult> GetLatestLoanOffers(string customerID)
        {
            return Ok(await _cs.GetCustomerLatestLoanOffers(customerID));
        }
        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<IActionResult> GetCustomerDetailsFromBankOne(string customerID)
        {
            return Ok(await _bi.GetByCustomerID(customerID));
        }
        //[HttpPost]
        //[Route("GetMonoAccountId")]
        //public IActionResult GetCustomerMonoAccountId([FromBody] AddAccountIdRequestDTO aard)
        //{
        //    return Ok(_mo.GetAccountIdFromMono(aard));
        //}

        [HttpGet]
        [Route("AddCardValidation")]
        public IActionResult AddCardValidation(string customerID)
        {
            return Ok(_bi.AddCardValidation(customerID));
        }

        [HttpPost]
        [Route("ValidateCardForTokenization")]
        public async Task<IActionResult> ValidateCustomerCardAsync(ValidateCardDetailsDTO vcd)
        {
            return Ok(await _cs.ValidateCardDetails(vcd));
        }

    }
}
