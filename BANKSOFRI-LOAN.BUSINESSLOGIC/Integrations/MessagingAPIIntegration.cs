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
  public  class MessagingAPIIntegration : IMessage
    {
        IConfiguration _config;
        ILogs _log;
        public MessagingAPIIntegration(IConfiguration config, ILogs log)
        {
            _config = config;
            _log = log;
        }
        public SendSMSResponse SendMessageToCustomer(SendSMSRequestObject or)
        {
            string username = _config.GetSection("UserName").Value;
            string URL = _config.GetSection("SMSbaseurl").Value + "/Messaging/SendSMS";
            string password = _config.GetSection("Password").Value;
            SendSMSResponse resp = new SendSMSResponse();
            try
            {
                or.Reference = GenReferenceNumber(11);
                var client = new RestClient(URL);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Basic " + ConvertLoginCredentials(username, password));
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(or);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<SendSMSResponse>(response.Content.ToString());
            }
            catch (Exception ex)
            {
                _log.ServiceMsg("An error occured on SendMessageToCustomer method! Details: " + ex.Message, "Error");
            }
            return resp;
        }
        string ConvertLoginCredentials(string username, string password)
            {
                var plaindetails = System.Text.Encoding.UTF8.GetBytes(username + ":" + password);
                return System.Convert.ToBase64String(plaindetails);
            }
            public static string GenReferenceNumber(int length)
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }

