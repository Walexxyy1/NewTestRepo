using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class ChargeCardObject
    {
        [Key]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Amount to Charge")]
        public decimal Amount { get; set; }
        public string CustomerId { get; set; }
        public string LoanRefId { get; set; }
    }
}
