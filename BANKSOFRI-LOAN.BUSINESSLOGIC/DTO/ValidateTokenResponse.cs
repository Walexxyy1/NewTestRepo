﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class ValidateTokenResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string CustomerDetails { get; set; }
        public string customerID { get; set; }
        public string Status { get; set; }
    }
}
