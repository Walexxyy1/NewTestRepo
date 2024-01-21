using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ILoanPro
    {
        Task<ProvenirCreditDecisionResponseObject> GetLoanOffersFromProvenir(ProvenirCreditDecisionRequestObject req);
        Task PersistCustomerOffers(CustomerOffer co);
    }
}
