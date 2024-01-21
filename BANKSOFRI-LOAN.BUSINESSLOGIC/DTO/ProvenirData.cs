using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class ProvenirData
    {
        private readonly ILogs _log;
        ApiauthorisationContext _ctx;
        public ProvenirData(ILogs log, ApiauthorisationContext ctx)
        {
            _log = log;
            _ctx = ctx;
        }
       
        public async Task<long> SaveProvenirData(ProvenirCreditDecisionResponseObject p)
        {
            long resp = 0;
            string ostatus = "Cancelled";
            try
            {
                if (p.overallDecision == "Accept") { ostatus = "PENDING"; }

                 await _ctx.LoanProcessingData.AddAsync(new LoanProcessingData()
                {
                    Date = DateTime.Now,
                    RequestId = p.ReferenceId,
                    CustomerId = p.clientId,
                    Offers = JsonConvert.SerializeObject(p.offers.ToString()),
                    ApplicationId = p.applicationId,
                    Guid = p.guid,
                    OverallDecision = p.overallDecision,
                    DeclineReasons = JsonConvert.SerializeObject(p.declineReasons.ToString()),
                    ApplicationXml = p.applicationXML,
                    Errors = JsonConvert.SerializeObject(p.errors.ToString()),
                    OfferStatus = ostatus,
                    EmploymentType = p.EmploymentType,
                    RiskLevel = p.riskLevel
                });
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on SaveProvenirData method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<LoanProcessingData> GetLatestCustomerProvenirData(string custID)
        {
            LoanProcessingData lp = new LoanProcessingData();
            try
            {
                DateTime maxdate = DateTime.Now.AddDays(-29);
                LoanProcessingData lpd = await _ctx.LoanProcessingData.Where(x => x.CustomerId == custID && x.OfferStatus == "PENDING").FirstOrDefaultAsync();
                if (lpd.Date <= maxdate)
                {
                    return lpd;
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetLatestCustomerProvenirData method! Details: " + ex.Message, "Error");
            }
            return lp;
        }
        public async Task<List<LoanProcessingData>> LoadAllProvenirData()
        {
            return await _ctx.LoanProcessingData.ToListAsync();
        }
        public async Task<LoanProcessingData> GetCustomerProvenirData(string referenceId)
        {
            LoanProcessingData lp = new LoanProcessingData();
            try
            {
                DateTime maxdate = DateTime.Now.AddDays(-29);
                //string sql = "SELECT TOP(1) * FROM LoanProcessingData WHERE RequestId=@rID ORDER BY ID DESC";
                //LoanProcessingData lpd = repo.Query<LoanProcessingData>(sql, new { rID = referenceId, status = "PENDING" });
                LoanProcessingData lpd = await _ctx.LoanProcessingData.Where(x => x.RequestId == referenceId && x.OfferStatus == "PENDING").FirstOrDefaultAsync();
                if (lpd.Date <= maxdate)
                {
                    return lpd;
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetCustomerProvenirData method! Details: " + ex.Message, "Error");
            }
            return lp;
        }

        public async Task UpdateCustomerProvenirOffer(string referenceId, string custID, string status)
        {
            LoanProcessingData lp = new LoanProcessingData();
            try
            {
                
                var loan = await _ctx.LoanProcessingData.Where(x => x.RequestId == referenceId && x.CustomerId == custID).FirstOrDefaultAsync();
                if(loan != null)
                {
                    loan.OfferStatus = status;
                    _ctx.Update(loan);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateCustomerProvenirOffer method! Details: " + ex.Message, "Error");
            }
        }
    }
}
