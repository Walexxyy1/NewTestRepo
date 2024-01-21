using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
   public class LienService : ILien
    {
        ILoanService _lserv;
        IBone _ib;
        ILogs _log;
        public LienService(ILoanService lserv, IBone bi, ILogs log)
        {
            _lserv = lserv;
            _ib = bi;
            _log = log;
        }

        public void UnlienAccountService()
        {
            _log.Logger($"UnLienAccountService has started...", "INFO");
            List<LienHistory> liens = _lserv.GetAllLienHistoryDue(DateTime.Today.ToShortDateString());
            if(liens != null)
            {
                _log.Logger($"Total Lien Records found due for today: {liens.Count()}", "INFO");
                foreach (LienHistory lien in liens)
                {
                    AccountlienResponse alr = _ib.RemoveLienOnAccount(new RemovelientOnAccountRequestObject() 
                    { 
                        AccountNo = lien.AccountNumber, 
                        Amount = lien.LienAmount.ToString(), 
                        AuthenticationCode = null, 
                        Reason = "Loan collection", 
                        ReferenceID = lien.LienReferenceId 
                    });
                    if(alr.RequestStatus == "true")
                    {
                        _lserv.UpdateLienHistoryStatus(lien.CustomerId, lien.LoanReferenceId, lien.RemoveDate.ToShortDateString());
                    }
                }
                _log.Logger($"UnLienAccountService stopped...", "INFO");
            }
            else
            {
                _log.Logger($"There are no lien Account due as at today", "INFO");
            }
        }
    }
}
