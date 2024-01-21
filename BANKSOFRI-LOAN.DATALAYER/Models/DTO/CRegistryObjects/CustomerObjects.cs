using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects
{
   public class CustomerResponseObjects
    {
        public List<SearchResult> SearchResult { get; set; }
        public bool Success { get; set; }
        public List<object> Errors { get; set; }
        public string InfoMessage { get; set; }
        public string TransactionID { get; set; }
    }

    public class SearchResult
    {
        public int Relevance { get; set; }
        public string RegistryID { get; set; }
        public string Name { get; set; }
    }

    public class GetCustomerBVNRequestObjects
    {
        public int MaxRecords { get; set; }
        public int MinRelevance { get; set; }
        public string SessionCode { get; set; }
        public string CustomerQuery { get; set; }
        public string GetNoMatchReport { get; set; }
        public string EnquiryReason { get; set; }
    }
    public class GetCreditScoreRequestObjects
    {        
        public string BVN { get; set; }
        public string LoanAmount { get; set; }
    }
}
