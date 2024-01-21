using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.AllaweeObjects;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class AllaweeIntegration : IAllInt
    {
        
        IConfiguration _config;
        string baseurl = null;
        string token = null;
        IRepository repo;
        public AllaweeIntegration(IConfiguration config, IRepository _repo)
        {
            _config = config;
            baseurl = _config.GetSection("AllaweeBaseUrl").Value;
            token = _config.GetSection("AllaweeCallToken").Value;
            repo = _repo;
        }
        AppLogger _log = new AppLogger();
       m

        public GetIdentityResponseObject GetCustomerIdentity(string bvn)
        {
            GetIdentityResponseObject resp = new GetIdentityResponseObject();
            
            try
            {               
                string url = baseurl + "/identity/bvn/" + bvn;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer "+token);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<GetIdentityResponseObject>(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the Allawee GetCustomer Identity! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public GetScoreResponseObject GetCustomerCreditScore(GetScoreRequestObject ccs)
        {
            CreditScoreData scd = GetMostRecentCreditScoreData(ccs.customerId);
            if(scd != null)
            {
                return JsonConvert.DeserializeObject<GetScoreResponseObject>(scd.ScoreData);
            }
            else
            {
                return GetCustomerCreditNewScore(ccs);
            }          
        }

        GetScoreResponseObject GetCustomerCreditNewScore(GetScoreRequestObject ccs)
        {
            GetScoreResponseObject resp = new GetScoreResponseObject();
            GetIdentityResponseObject gir = GetCustomerIdentity(ccs.BVN);
            if (gir.data != null)
            {
                try
                {
                    string url = baseurl + "/credit?customerId=" + gir.data.customerId + "&fields=score";
                    var client = new RestClient(url);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Bearer " + token);
                    IRestResponse response = client.Execute(request);
                    resp = JsonConvert.DeserializeObject<GetScoreResponseObject>(response.Content);
                    SaveCreditScore(ccs.customerId, response.Content.ToString());
                }
                catch (Exception ex)
                {
                    _log.Logger("An error occurred on the Allawee GetCustomerCreditScore! Details: " + ex.Message, "ERROR");
                }
            }
            return resp;
        }
        void SaveCreditScore(string CustId, string scoredata)
        {
            try
            {
                repo.InsertAsync(new CreditScoreData() { Date = DateTime.Today, CustomerId = CustId, ScoreData = scoredata });
            }catch(Exception ex)
            {
                _log.Logger("An error occurred on the SaveCreditScore method in AllwaeeIntegration Class! Details: " + ex.Message, "ERROR");
            }
        }
        CreditScoreData GetMostRecentCreditScoreData(string customerId)
        {
            CreditScoreData scd = new CreditScoreData();
            try
            {
                string sql = "SELECT TOP 1 * FROM CreditScoreData WHERE CustomerId=@custId Order by Id desc";
                scd = repo.Query<CreditScoreData>(sql, new { custId = customerId });
                if (scd != null)
                {
                    if ((scd.Date - DateTime.Today).TotalDays < 30)
                    {
                        return scd;
                    }
                }
            }
            catch(Exception ex)
            {
                _log.Logger("An error occurred on the GetMostRecentCreditScoreData method in AllwaeeIntegration Class! Details: " + ex.Message, "ERROR");
            }
            return scd;
        }
    }
}
