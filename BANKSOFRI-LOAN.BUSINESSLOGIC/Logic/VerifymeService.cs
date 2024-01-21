using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class VerifymeService : IVerify
    {
        IConfiguration _config;
        ILogs _log;
        ApiauthorisationContext _ctx;
        public VerifymeService(IConfiguration config, ILogs log, ApiauthorisationContext ctx)
        {
            _config = config;
            _log = log;
            _ctx = ctx;
        }
        
        public async Task<VerifyResponseObj> SubmitAddressVerificationRequest(VerifyRequestObj req)
        {
            VerifyResponseObj verifyaddressResponse = new VerifyResponseObj();
            try
            {
                string url = _config.GetSection("verifymeAddressRequestUrl").Value; //ConfigurationManager.AppSettings["verifymeAddressRequestUrl"];
                string token = _config.GetSection("verifymetestkey").Value; 
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(req);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                verifyaddressResponse = JsonConvert.DeserializeObject<VerifyResponseObj>(response.Content);
                //if(response.IsSuccessful)
                //{
               await _ctx.VerificationRequests.AddAsync(new VerificationRequest()
                {
                    Date = DateTime.Now,
                    Firstname = req.applicant.firstname,
                    Lastname = req.applicant.lastname,
                    Street = req.street,
                    Lga = req.lga,
                    State = req.state,
                    Landmark = req.landmark,
                    IdType = req.applicant.idType,
                    IdNumber = req.applicant.idNumber,
                    Phone = req.applicant.phone,
                    Dob = req.applicant.dob,
                    VerificationStatus = "PENDING"
                });
               await _ctx.SaveChangesAsync();
                //}
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on SubmitAddressVerificationRequest in VerifyMeService Class {ex.Message}", "Error");
            }
            return verifyaddressResponse;
        }

        public async Task<CancelVerifyResponse> CancelAddressVerification(string Id)
        {
            CancelVerifyResponse verifyaddressResponse = new CancelVerifyResponse();
            try
            {
               
                string url = _config.GetSection("canceladdressverifyurl").Value; //ConfigurationManager.AppSettings["verifymeAddressRequestUrl"];
                string token = _config.GetSection("verifymetestkey").Value;
                var client = new RestClient(url + "/" + Id);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                verifyaddressResponse = JsonConvert.DeserializeObject<CancelVerifyResponse>(response.Content);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
               _log.Logger($"An error occured on CancelAddressVerificationin Verifymeservice. Details:{ex.Message} ", "Error");
            }
            return verifyaddressResponse;
        }

        public async Task<GetVerificationResponse> GetVerificationResponse(GetVerificationRequesObj g)
        {
            GetVerificationResponse verifystatus = new GetVerificationResponse();
            try
            {
                //string sql = "SELECT * FROM VerificationRequest WHERE firstname=@first AND lastname=@last AND phone=@phone";
                //VerificationRequest vr = _rep.Query<VerificationRequest>(sql, new { first = g.firstName, last = g.LastName, phone = g.PhoneNumber });
                VerificationRequest vr = await _ctx.VerificationRequests.Where(x => x.Firstname == g.firstName && x.Lastname == g.LastName && x.Phone == g.PhoneNumber).FirstOrDefaultAsync(); 
                if (vr != null)
                {
                    verifystatus = new GetVerificationResponse() { ResponseCode = "00", ResponseMessage = "Success!", VerificationStatus = vr.VerificationStatus };
                }
                else
                {
                    verifystatus = new GetVerificationResponse() { ResponseCode = "11", ResponseMessage = "Something Went Wrong!", VerificationStatus = null };
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on GetVerificationResponse Verifymeservice. Details:{ex.Message} ", "Error");
                verifystatus = new GetVerificationResponse() { ResponseCode = "11", ResponseMessage = "Something Went Wrong!", VerificationStatus = null };
            }
            return verifystatus;
        }
    }
}
