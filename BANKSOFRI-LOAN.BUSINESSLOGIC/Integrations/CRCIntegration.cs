using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
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
    public class CRCIntegration : ICRC
    {
       ILogs _log;
        IConfiguration _config;
        string baseurl = null;       
        string CRCUSER = null;
        string CRCPASS = null;
        ICustomer _cust;
        public CRCIntegration(IConfiguration config, ICustomer cust, ILogs log)
        {
            _log = log;
            _config = config;
            baseurl = _config.GetSection("CRCBaseUrl").Value;           
            CRCUSER = _config.GetSection("CRCUsername").Value;
            CRCPASS = _config.GetSection("CRCPassword").Value;
            _cust = cust; 
        }
        public CRCResponseUpdated GetCRCDetails(string custBvn)
        {
            CRCResponseUpdated resp = new CRCResponseUpdated();
            try
            {
                string req = "{'@REQUEST_ID': '1','REQUEST_PARAMETERS': {'REPORT_PARAMETERS': {'@REPORT_ID': '7461','@SUBJECT_TYPE': '1','@RESPONSE_TYPE': '5'},'INQUIRY_REASON': {      '@CODE': '1'   },   'APPLICATION': {      '@PRODUCT': '017',      '@NUMBER': '232',      '@AMOUNT': '15000',      '@CURRENCY': 'NGN'   }},'SEARCH_PARAMETERS': {   '@SEARCH-TYPE': '4',   'BVN_NO': '"+custBvn +"' }}";
                CRCRequest crcreq = new CRCRequest() { Request = req, UserName = CRCUSER, Password = CRCPASS };
                var client = new RestClient(baseurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(crcreq);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                dynamic d = JsonConvert.DeserializeObject<dynamic>(response.Content);
                if(d.ConsumerHitResponse != null)
                {
                    resp = JsonConvert.DeserializeObject<CRCResponseUpdated>(response.Content);
                }
               
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on the CRCIntegration GetCustomerCreditFromCRC! Details: {ex.Message}", "ERROR");
            }
            return resp;
        }
        public async Task<CRCSCResponse> GetCRCCreditScoreAsync(string custBvn, string customerId)
        {
            CRCSCResponse reply = new CRCSCResponse();
            CRCResponseUpdated resp = new CRCResponseUpdated();
            var repp = await _cust.GetMostRecentCreditBureauScore(customerId, "CRC");
            if (repp != null)
            {
                resp = JsonConvert.DeserializeObject<CRCResponseUpdated>(repp.ScoreData);
                reply = new CRCSCResponse() { ResponseCode = "00", ResponseMessage = "SUCCESS!", ScoreDetails = resp };
            }
            else
            {
                CRCResponseUpdated crcresp = GetCRCDetails(custBvn);
                reply = new CRCSCResponse()
                {
                    ResponseCode = "00",
                    ResponseMessage = "SUCCESS!",
                    ScoreDetails = crcresp
                };
            }
            return reply;
        }
    }
}
