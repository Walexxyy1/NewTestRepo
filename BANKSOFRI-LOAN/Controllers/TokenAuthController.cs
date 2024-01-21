using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class TokenAuthController : ControllerBase
    {
        IUserAuth _uth;
        private readonly ICustomer cs;

        public TokenAuthController(IUserAuth uth, ICustomer _cs)
        {
            _uth = uth;
            cs = _cs;
        }
       
       
        [HttpPost]
        [Route("GetUserAuthToken")]
        public async Task<IActionResult> GetUserAuthToken([FromBody] AuthDTO ad)
        {
            string authtoken = await _uth.GetAuthToken(ad);
            if (authtoken != null)
            {
                return Ok(new GetUserTokenResponse() { ResponseCode = "00", ResponseMessage = "Success", Token = authtoken });
            }
            else
            {
                return Ok(new GetUserTokenResponse() { ResponseCode = "01", ResponseMessage = "Failed Request", Token = null });
            }
        }
       
        [HttpPost]
        [Route("ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromBody] AuthValidationDTO avdto)
        {
            string result = await _uth.UserAuthTokenValidation(avdto);
            if (result != null)
            {
                string custdetails = await cs.GetCustomerDetails(avdto.Email);
                return Ok(new ValidateTokenResponse() { ResponseCode = "00", ResponseMessage = "Success", Status = "VALID", CustomerDetails = custdetails, customerID = result });
            }
            else
            {
                return Ok(new ValidateTokenResponse() { ResponseCode = "01", ResponseMessage = "FAILED", Status = "INVALID", customerID = result });
            }
        }
    }
}
