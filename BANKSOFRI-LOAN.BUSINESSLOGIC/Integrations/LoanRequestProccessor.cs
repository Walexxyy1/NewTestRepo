using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class LoanRequestProccessor : ILoanPro
    {
        IConfiguration _config;        
        ILogs _logs;
        ApiauthorisationContext _ctx;
        public LoanRequestProccessor(IConfiguration config, ILogs log, ApiauthorisationContext ctx)
        {
            _config = config;
           _logs = log;
            _ctx = ctx;
        }
        public async Task<ProvenirCreditDecisionResponseObject> GetLoanOffersFromProvenir(ProvenirCreditDecisionRequestObject req)
        {
            ProvenirCreditDecisionResponseObject resp = new ProvenirCreditDecisionResponseObject();
            try
            {
                string url = _config.GetSection("ProvenirCreditDecitionUrl").Value; 
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);               
                request.AddHeader("Content-Type", "application/json");
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(req);
               _logs.Logger("Calling Provenir Decision for customer ID: "+ req.clientId +" With payload: "+ body, "INFO");
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);                 
                resp = Newtonsoft.Json.JsonConvert.DeserializeObject<ProvenirCreditDecisionResponseObject>(response.Content.ToString());
                await PersistDecisionData(new ProvenirDecisionData() { Date = DateTime.Now, CustomerId = req.clientId, ReferenceId = req.applicationId, LoanData = response.Content.ToString(), Status = "PENDING"});
            }
            catch (Exception ex)
            {
                _logs.Logger(ex.Message, "Error");
            }
            return resp;
        }
        async Task PersistDecisionData(ProvenirDecisionData pdd)
        {
            try
            {
               await _ctx.ProvenirDecisionData.AddAsync(pdd);
            }catch(Exception ex)
            {
                _logs.Logger($"An error occurred on PersistDecisionData method in LoanRquestProccessors {ex.Message}", "Error");
            }
        }

       public async Task PersistCustomerOffers(CustomerOffer co)
        {
            try
            {
               await DeleteExistingCustomreOffers(co.CustomerId);
               await _ctx.CustomerOffers.AddAsync(co);
            }
            catch (Exception ex)
            {
                _logs.Logger($"An error occured in PersistCustomerOffers in LoanRequestProcessor class. Details {ex.Message}", "ERROR");
            }
        }

        async Task DeleteExistingCustomreOffers(string customerId)
        {
            try
            {
               
                CustomerOffer c = _ctx.CustomerOffers.Where(x => x.CustomerId == customerId).FirstOrDefault();
                if(c != null)
                {
                    _ctx.CustomerOffers.Remove(c);
                   await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logs.Logger($"An error occured in PersistCustomerOffers in LoanRequestProcessor class. Details {ex.Message}", "ERROR");
            }
        }
    }
}
