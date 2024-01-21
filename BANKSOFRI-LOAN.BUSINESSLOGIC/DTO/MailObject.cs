using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class MailObject
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Fullname")]
        public string FirstName { get; set; }
        [Display(Name = "Email Address")]
        public string CustomerId { get; set; }
        public string LoanAmount { get; set; }
    }
}
