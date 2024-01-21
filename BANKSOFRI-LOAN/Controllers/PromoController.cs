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
    public class PromoController : ControllerBase
    {
        IPromo _pro;
        public PromoController(IPromo pro)
        {
            _pro = pro;
        }
        [HttpPost]
        [Route("GetBonus")]
        public async Task<IActionResult> GetPromoBonus([FromBody] PromotionRequestObject pr)
        {
            return Ok(await _pro.HandlePromotions(pr));
        }
        [HttpGet]
        [Route("GetHistory")]
        public IActionResult GetCustomerPromoUsage(string customerId)
        {
            return Ok(_pro.GetPromoUsage(customerId));
        }
    }

}
