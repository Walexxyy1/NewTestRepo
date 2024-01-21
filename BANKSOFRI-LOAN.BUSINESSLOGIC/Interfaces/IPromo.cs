using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface IPromo
    {
        Task<ResponseObject> HandlePromotions(PromotionRequestObject prc);
        BonusHistoryResponse GetPromoUsage(string customerId);
        CustomerDetailsDTO GetByCustomerID(string customerID);
    }
}
