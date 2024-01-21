using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects
{
    public class AddAccountIdResponse
    {
        public string id { get; set; }
    }
    public class AddAccountIdRequestDTO
    {
        public string CustomerId { get; set; }
        public string code { get; set; }
    }
    public class AddAccountIdRequest
    {
        public string code { get; set; }
    }
}
