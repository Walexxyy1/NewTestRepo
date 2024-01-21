using BANKSOFRI_LOAN.DATALAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class GetCustomerLoanRequest:ResponseObject
    {
        public List<NanoLoan> Loans { get; set; }
    }
}
