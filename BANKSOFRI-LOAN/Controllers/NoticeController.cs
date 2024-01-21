using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
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
    public class NoticeController : ControllerBase
    {
        private readonly INotice sms;
        private readonly ICRC _crc;
        private readonly ICRegistry _creg;
        private readonly ICBScore _sc;

        public NoticeController(INotice _sms, ICRC crc, ICRegistry creg, ICBScore sc)
        {
            sms = _sms;
            _crc = crc;
            _creg = creg;
            _sc = sc;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerCRCCreditScoreAsync(string customerBVN, string customerId)
        {
            //await sms.Send24HoursNotification();
            CRCSCResponse ccsc = await _crc.GetCRCCreditScoreAsync(customerBVN, customerId);
            return Ok(ccsc.ScoreDetails.ConsumerHitResponse.BODY.CREDIT_SCORE_DETAILS.CREDIT_SCORE_SUMMARY.CREDIT_SCORE);
        }
        [HttpPost]
        [Route("CreditRegistryScore")]
        public IActionResult GetCustomerCreditRegsitryCreditScore([FromBody] CRegisteryCreditScoreRequestDTO gc)
        {
            //await sms.Send24HoursNotification();
            //CRCSCResponse ccsc = ;
            return Ok(_creg.GetCustomerCreditScoreAsync(gc));
        }

        [HttpPost]
        [Route("GetCustomerCreditScore")]
        public IActionResult GetAverageScore([FromBody] CRegisteryCreditScoreRequestDTO gc)
        {
            //await sms.Send24HoursNotification();
            //CRCSCResponse ccsc = ;
            return Ok(_sc.GetCreditScore(gc));
        }
    }
}
