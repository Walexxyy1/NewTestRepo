using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.DTO
{
    public class GetCustomerCardStatusRequest
    {
        public string CustomerId { get; set; }
        public string  token { get; set; }
    }
}
