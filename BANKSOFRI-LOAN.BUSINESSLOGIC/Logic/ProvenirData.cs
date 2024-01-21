using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class ProvenirData : IProvenir
    {
        
        IConfiguration _cfg;
        ILogs _log;
        ApiauthorisationContext _ctx;
        public ProvenirData( IConfiguration cfg, ILogs log, ApiauthorisationContext ctx)
        {
         _log = log;
            _cfg = cfg;
            _ctx = ctx;
        }
        
        public async Task<long> SaveProvenirData(ProvenirCreditDecisionResponseObject p)
        {
            long resp = 0;
            string ostatus = "Cancelled";
            string dreason = null;
            string offers = null;
            string error = null;
            try
            {
                if (p.overallDecision == "Accept") { ostatus = "PENDING";}
                if(p.offers != null)
                {
                   offers = JsonConvert.SerializeObject(p.offers.ToString());
                }
                if(p.errors != null)
                {
                    error = JsonConvert.SerializeObject(p.errors.ToString());
                }
                if(p.declineReasons != null)
                {
                    dreason = JsonConvert.SerializeObject(p.declineReasons.ToString());
                }
               
                await _ctx.LoanProcessingData.AddAsync(new LoanProcessingData()
                {
                    Date = DateTime.Now,
                    RequestId = p.applicationId,
                    CustomerId = p.clientId,
                    Offers = offers,
                    ApplicationId = p.applicationId,
                    Guid = p.guid,
                    OverallDecision = p.overallDecision,
                    DeclineReasons = dreason,
                    ApplicationXml = p.applicationXML,
                    Errors = error,
                    OfferStatus = ostatus,
                    EmploymentType = p.EmploymentType,
                    RiskLevel = p.riskLevel
                });
                resp = await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on SaveProvenirData method! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public async Task<CustomerOffer> GetLatestCustomerProvenirData(string custID)
        {
            CustomerOffer lp = new CustomerOffer();
            int validays = int.Parse(_cfg.GetSection("OfferValidDays").Value);
            try
            {
                DateTime maxdate = DateTime.Now.AddDays(-validays);
               //string sql = "SELECT TOP 1 * FROM CustomerOffers WHERE CustomerID=@cID ORDER BY ID DESC";
                CustomerOffer lpd = await _ctx.CustomerOffers.Where(x => x.CustomerId == custID).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                if (lpd != null)
                {
                    if (lpd.Date >= maxdate)
                    {
                        return lpd;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on GetLatestCustomerProvenirData method! Details: {ex.Message}", "ERROR");
            }
            return lp;
        }
        public async Task<List<LoanProcessingData>> LoadAllProvenirData()
        {
            return await _ctx.LoanProcessingData.ToListAsync(); ;
        }
        public async Task<LoanProcessingData> GetCustomerProvenirData(string referenceId)
        {
            LoanProcessingData lp = new LoanProcessingData();
            try
            {
                DateTime maxdate = DateTime.Now.AddDays(-29);
                //string sql = "SELECT TOP(1) * FROM LoanProcessingData WHERE RequestId=@rID ORDER BY ID DESC";
                LoanProcessingData lpd = await _ctx.LoanProcessingData.Where(x => x.RequestId == referenceId).LastOrDefaultAsync(); 
                if (lpd != null)
                {
                    if (lpd.Date <= maxdate)
                    {
                        return lpd;
                    }
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
                //string sql = "UPDATE LoanProcessingData SET Status = @status WHERE RequestId=@rID AND CustomerId=@cId ORDER BY ID DESC";
                //repo.Query<LoanProcessingData>(sql, new { rID = referenceId, status = status, cId = custID });
                LoanProcessingData ld = await _ctx.LoanProcessingData.Where(x => x.RequestId == referenceId && x.CustomerId == custID).FirstOrDefaultAsync();
                if(ld != null)
                {
                    ld.OfferStatus = status;
                    _ctx.Update(ld);
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
