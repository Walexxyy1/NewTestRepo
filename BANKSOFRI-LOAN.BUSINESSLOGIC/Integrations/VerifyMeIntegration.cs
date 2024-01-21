using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class VerifyMeIntegration : IVerifyBvn
    {
        IConfiguration _config;
        ILogs _log;
        string baseurl = null;
        string authtoken = null;
        ApiauthorisationContext _ctx;
        public VerifyMeIntegration(IConfiguration config, ILogs log, ApiauthorisationContext ctx)
        {
            _config = config;
            _log = log;
            baseurl = _config.GetSection("verifymeBVNValidationBaseUrl").Value;
            authtoken = _config.GetSection("verifymeliveKey").Value;
            _ctx = ctx;
        }        
       
        public VerifymeBVNResponse ValidateBVNOnVerifyMe(VerifyMeBVNValidationRequestObject vb)
        {
            VerifymeBVNResponse resp = new VerifymeBVNResponse();
            try
            {
                BvnData cbd = getCustomerBVNData(vb.CustomeId);
                if (cbd.bvn == null)
                {
                    _log.Logger($"No bvn details found for customer { vb.CustomeId }. Calling VerifyMe API for customer bvn data ", "INFO");
                    string url = baseurl + "/identities/bvn/" + vb.BVN + "?type=premium";
                    var client = new RestClient(url);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + authtoken);
                    var body = JsonConvert.SerializeObject(new { firstname = vb.firstname, lastname = vb.lastname, dob = vb.dob });
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    resp = JsonConvert.DeserializeObject<VerifymeBVNResponse>(response.Content);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created && resp != null && resp.data != null)
                    {
                        SaveCutomerBVNData(resp, vb.CustomeId);
                    }
                   _log.Logger($"VerifyMe BVN validation Response for {vb.firstname}{vb.lastname} Details: {response.Content}", "INFO");
                }
                else
                {
                    resp = new VerifymeBVNResponse() { status = "Success", data = cbd };
                    _log.Logger($"Customer { vb.CustomeId } bvn data fetch successfully from the DB", "INFO");
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the ValidateBVNOnVerifyMe! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public BvnData getCustomerBVNData(string customerId)
        {
            BvnData cbd = new BvnData();
            _log.Logger($"Seaching the customer bvn table in the database for customer { customerId } ", "INFO");
            try
            {               
                var d = _ctx.CustomerBvndata.Where(x => x.CustomerId == customerId).FirstOrDefault();
                if(d != null)
                {
                    cbd = new BvnData() 
                    {                        
                        bvn = d.Bvn,
                        firstname = d.Firstname,
                        lastname = d.Lastname,
                        middlename = d.Middlename,
                        gender = d.Gender,
                        phone = d.Phone,
                        photo = d.Photo,
                        birthdate = d.Birthdate,
                        title = d.Title,
                        stateOfOrigin = d.StateOfOrigin,
                        maritalStatus = d.MaritalStatus,
                        residentialAddress = d.ResidentialAddress,
                        levelOfAccount = d.LevelOfAccount,
                        lgaOfResidence = d.LgaOfResidence,
                        lgaOfOrigin = d.LgaOfOrigin,
                        nameOnCard = d.NameOnCard,
                        enrollmentBank = d.EnrollmentBank,
                        enrollmentBranch = d.EnrollmentBranch
                    };
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on the getCustomerBVNData! Details:  {ex.Message}", "ERROR");
            }
            return cbd;
        }

        public void SaveCutomerBVNData(VerifymeBVNResponse reply, string customerId)
        {
            var d = reply.data;
            try 
            {
                _ctx.CustomerBvndata.Add(new CustomerBvndata() 
               { 
                   Date = DateTime.Now,
                   CustomerId = customerId,
                   Bvn = d.bvn,
                   Firstname = d.firstname, 
                   Lastname = d.lastname, 
                   Middlename = d.middlename, 
                   Gender = d.gender, 
                   Phone = d.phone, 
                   Photo = d.photo,
                   Birthdate = d.birthdate, 
                   Title = d.title, 
                   StateOfOrigin = d.stateOfOrigin, 
                   MaritalStatus = d.maritalStatus, 
                   ResidentialAddress = d.residentialAddress, 
                   LevelOfAccount = d.levelOfAccount, 
                   LgaOfResidence = d.lgaOfResidence, 
                   LgaOfOrigin = d.lgaOfOrigin, 
                   NameOnCard = d.nameOnCard, 
                   EnrollmentBank = d.enrollmentBank, 
                   EnrollmentBranch = d.enrollmentBranch                   
               });
            }
            catch(Exception ex)
            {
                _log.Logger($"An error occurred on the SaveCustomerBVNData! Details:  {ex.Message}", "ERROR");
            }            
        }
    }
}
