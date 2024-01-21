using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
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
    public class TestingController : ControllerBase
    {
        private readonly ICRC _crc;
        IVerifyBvn _vfy;
        private readonly ICRegistry _cry;

        public TestingController(ICRC crc, IVerifyBvn vfy, ICRegistry cry)
        {
            _crc = crc;
            _vfy = vfy;
            _cry = cry;
        }
        [HttpGet]
        [Route("TestCRC")]
        public async Task<IActionResult> CRCTest(string customerID, string BVN)
        {
            return Ok(await _crc.GetCRCCreditScoreAsync(BVN, customerID));
        }

        [HttpPost]
        [Route("TestCRegistry")]
        public async Task<IActionResult> CRegistryTest(CRegisteryCreditScoreRequestDTO css)
        {
            return Ok(await _cry.GetCustomerCreditScoreAsync(css));
        }

        [HttpPost]
        [Route("TestBVNVerifier")]
        public IActionResult VerifyBVN(DATALAYER.Models.DTO.VerifyMeBVNValidationRequestObject vm)
        {
            return Ok(_vfy.ValidateBVNOnVerifyMe(vm));
        }
       
    }
}
