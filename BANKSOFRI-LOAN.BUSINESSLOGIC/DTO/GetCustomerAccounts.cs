using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class GetCustomerAccounts 
    {       
        public List<AccountDetails> Accounts { get; set; }
    }
    public class AccountDetails
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string NUBAN { get; set; }
        public string ProductName { get; set; }
        public object ProductCode { get; set; }
        public string BranchCode { get; set; }
        public string CustomerID { get; set; }
        public int Status { get; set; }
        public string AccountTier { get; set; }
        public double LedgerBalance { get; set; }
        public double AvailableBalance { get; set; }
        public double WithdrawableAmount { get; set; }
    }
}
