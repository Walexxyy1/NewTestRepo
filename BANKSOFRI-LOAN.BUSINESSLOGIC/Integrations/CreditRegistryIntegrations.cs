using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using System.Collections.Generic;
using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class CreditRegistryIntegrations : ICRegistry
    {
        ILogs _log;
        IConfiguration _config;
        string baseurl = null;
        string subscriberID = null;
        string email = null;
        string passw = null;
        private readonly ICustomer _icust;

        public CreditRegistryIntegrations(IConfiguration config, ICustomer icust, ILogs log)
        {
            _log = log;
            _config = config;
             baseurl = _config.GetSection("CreditRegistryBaseUrl").Value;
             subscriberID = _config.GetSection("CreditRegistrySubscriberID").Value; 
             email = _config.GetSection("CreditRegistryEmailAddress").Value; 
             passw = _config.GetSection("CreditRegistryUserPassword").Value;
            _icust = icust;
        }
       
        private LoginResponseObject Login()
        {
            LoginRequestObject lr = new LoginRequestObject()
            {
                EmailAddress = email,
                Password = passw,
                SubscriberID = subscriberID
            };
            LoginResponseObject resp = new LoginResponseObject();
            try
            {
                var client = new RestClient(baseurl + "/Login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(lr);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<LoginResponseObject>(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the CreditRegistry Login! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public CustomerResponseObjects GetCustomer(GetCustomerBVNRequestObjects gc)
        {
            CustomerResponseObjects resp = new CustomerResponseObjects();
            try
            {              
                var client = new RestClient(baseurl + "/FindSummary");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(gc);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<CustomerResponseObjects>(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the CreditRegistry GetCustomer! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public async Task<Credit202Response> GetCustomerCreditScoreAsync(CRegisteryCreditScoreRequestDTO crsc)
        {
            Credit202Response resp = new Credit202Response();
            var repp = await _icust.GetMostRecentCreditBureauScore(crsc.CustomerId, "Credit Registry");
            if (repp != null)
            {
                resp = JsonConvert.DeserializeObject<Credit202Response>(repp.ScoreData);
            }
            else
            {
                LoginResponseObject ln = Login();
                CustomerResponseObjects cd = GetCustomer(new GetCustomerBVNRequestObjects() { CustomerQuery = crsc.BVN, GetNoMatchReport = "IfNoMatch", SessionCode = ln.SessionCode, EnquiryReason = "PersonalLoan" });
                if (cd.SearchResult.Count > 0)
                {
                    try
                    {
                        
                        List<string> reqIds = new List<string>(new string[] { cd.SearchResult[0].RegistryID.ToString() });                       
                       
                        CustomerCreditScoreRequestObj reqobj = new CustomerCreditScoreRequestObj() 
                        { 
                            CustomerRegistryIDList = reqIds, 
                            EnquiryReason = crsc.CreditScoreEnquiryReason,  
                            SessionCode = ln.SessionCode
                        };

                        var client = new RestClient(baseurl + "/GetReport202");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        var body = JsonConvert.SerializeObject(reqobj);
                        request.AddJsonBody(body);
                        IRestResponse response = client.Execute(request);
                        resp = JsonConvert.DeserializeObject<Credit202Response>(response.Content);
                        if (response.IsSuccessful && response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            await _icust.SaveCreditBureau(new CreditScoreData() { Date = DateTime.Today, CustomerId = crsc.CustomerId, Bureau = "Credit Registry", ScoreData = response.Content });
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Logger("An error occurred on the CreditRegistry GetCustomerCreditScore! Details: " + ex.Message, "ERROR");
                    }
                }
                else
                {
                    resp = new Credit202Response() { Success = false, InfoMessage = "No record found for this customer" };
                }
            }
            return resp;
        }

        
    }
}
