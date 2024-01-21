using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class BankOneLoanProccessing : IBoneLoan
    {
        IConfiguration _config;
        string bearerToken = null;
        string institutionCode = null;
        string URL = null;        
        string apiversion = null;
        ILogs _log;
        public BankOneLoanProccessing(IConfiguration config, ILogs log)
        {
            _config = config;
            bearerToken = _config.GetSection("bankOneServiceCalltoken").Value;
            institutionCode = _config.GetSection("institutionCode").Value;
            URL = _config.GetSection("bankOneAPIbaseurl").Value;
            apiversion = _config.GetSection("apiVersion").Value;
            _log = log;
        }
       
        public async Task<BankOneLoanRequestResponse> CreateLoanOnBankOne(LoanRequestObject req)
        {
            BankOneLoanRequestResponse resp = new BankOneLoanRequestResponse();
            try
            {

                var client = new RestClient(URL);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request = new RestRequest("/LoanApplication/LoanCreationApplication2/2", Method.POST);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddQueryParameter("version", apiversion);
                request.AddQueryParameter("mfbCode", institutionCode);
                string requestBody = JsonConvert.SerializeObject(req);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(requestBody);
                var response = client.Execute<BankOneLoanRequestResponse>(request);
                resp = JsonConvert.DeserializeObject<BankOneLoanRequestResponse>(response.Content.ToString());
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured in CreateLoanOnBankOne method on BankOneLoanProccessing class. Details:{ex.Message}", "Error");
            }
            return resp;
        }
    }
}
