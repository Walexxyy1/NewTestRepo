using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
  public interface IProvenir
    {
        Task<long> SaveProvenirData(ProvenirCreditDecisionResponseObject p);
        Task<CustomerOffer> GetLatestCustomerProvenirData(string custID);
        Task<List<LoanProcessingData>> LoadAllProvenirData();
        Task<LoanProcessingData> GetCustomerProvenirData(string referenceId);
        Task UpdateCustomerProvenirOffer(string referenceId, string custID, string status);
    }
}
