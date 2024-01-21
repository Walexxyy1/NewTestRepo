using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
   public class MonoIntegration : IMoint
    {
        ILogs _log;
        IConfiguration _config;
        IMono _mo;
        public MonoIntegration(IConfiguration config, IMono mo, ILogs log)
        {
            _log = log;
            _config = config;
            _mo = mo;
        }

        public ResponseObject GetAccountIdFromMono(AddAccountIdRequestDTO req)
        {
            ResponseObject resobj = new ResponseObject();
            AddAccountIdResponse result = new AddAccountIdResponse();
            _log.Logger($"Attempting to get customer: {req.CustomerId} Mono Account id", "INFO");
            try
            {
                string appurl = _config.GetSection("MonoAccountIdUrl").Value;
                string monosecretKey = _config.GetSection("MonoSecretKey").Value;               
                var client = new RestClient(appurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("mono-sec-key", monosecretKey);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(new AddAccountIdRequest() { code = req.code }); 
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                result = JsonConvert.DeserializeObject<AddAccountIdResponse>(response.Content.ToString());
                _log.Logger($"customer mono AccountId response: {JsonConvert.SerializeObject(response.Content)}", "INFO");
                if(response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
                {
                    //_mo.SaveCustomerMonoAccountId(new MonoAccount() { = DateTime.Now, CustomerId = req.CustomerId, MonoAccountId = result.id });
                    return new ResponseObject() { ResponseCode = "00", ResponseMessage = "SUCCESS! Customer Mono Account Details fetched successfully!" };
                }
                resobj = new ResponseObject() { ResponseCode = "01", ResponseMessage = $"FAILED! Could not get Customer Mono Account Details!{JsonConvert.SerializeObject(result)}" };
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on GetAccountIdFromMono method in MonoIntegration class! Details: { ex.Message}", "ERROR");
            }
            return resobj;
        }

        public MonoAccountDetailsResponse GetCustomerMonoAccountDetails(MonoAccountDetailsRequest req)
        {
            MonoAccountDetailsResponse result = new MonoAccountDetailsResponse();
            _log.Logger($"Attempting to get customer: {req.CustomerId} Mono Account details", "INFO");
            try
            {
                string monosecretKey = _config.GetSection("MonoSecretKey").Value;
                string appurl = $"{_config.GetSection("MonoAccountDetailsUrl").Value}/{req.AccountId}/income";
               
                var client = new RestClient(appurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("mono-sec-key", monosecretKey);                
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                result = JsonConvert.DeserializeObject<MonoAccountDetailsResponse>(response.Content.ToString());
                _log.Logger($"Customer mono Account details response: {JsonConvert.SerializeObject(response.Content)}", "INFO");               
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on GetCustomerMonoAccountDetails method in MonoIntegration class! Details: { ex.Message}", "ERROR");
            }
            return result;
        }

        public MonoStatementResponse GetEmployedCustomerMonoAccountDetails(MonoAccountDetailsRequest req)
        {
            MonoStatementResponse result = new MonoStatementResponse();
            _log.Logger($"Attempting to get customer: {req.CustomerId} Mono Account details", "INFO");
            try
            {
               // string url = "https://api.withmono.com/accounts/62b2d5f6f5c89628113e0dd6/statement?period=3";
                string monosecretKey = _config.GetSection("MonoSecretKey").Value;
                string appurl = $"{_config.GetSection("MonoAccountDetailsUrl").Value}/{req.AccountId}/statement?period=last3months";

                var client = new RestClient(appurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("mono-sec-key", monosecretKey);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                result = JsonConvert.DeserializeObject<MonoStatementResponse>(response.Content.ToString());
                _log.Logger($"Customer mono Account details response: {JsonConvert.SerializeObject(response.Content)}", "INFO");
                List<ResponseData> income = result.data.Where(x => x.narration.Contains("STAFF SALARY") || x.narration.Contains("SALARY")).ToList();
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on GetEmployedCustomerMonoAccountDetails method in MonoIntegration class! Details: { ex.Message}", "ERROR");
            }
            return result;
        }
    }
}
