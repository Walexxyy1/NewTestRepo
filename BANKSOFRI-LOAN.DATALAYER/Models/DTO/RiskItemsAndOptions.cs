using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class RiskItemsAndOptions
    {
        public string RiskItem { get; set; }
        public List<string> Options { get; set; }
    }
}
