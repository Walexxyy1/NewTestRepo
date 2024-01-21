using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
   public class MetaIntegration : IMeta
    {
        IConfiguration _config;
        string username;
        string password;
        string metaUrl;
        ILogs _log;
        string flowId;
        private readonly IHostingEnvironment _env;

        public MetaIntegration(IConfiguration config, IHostingEnvironment env, ILogs log)
        {
            _config = config;
            username = _config.GetSection("MetaUsername").Value;
            password = _config.GetSection("MetaPassword").Value;
            metaUrl = _config.GetSection("MetaBaseUrl").Value;
            flowId = _config.GetSection("MetaFlowId").Value;
            _log = log;           
            _env = env;
        }
       private string MetaAuthentication()
        {
            string autorizationtoken = null;
            try {
                var client = new RestClient($"{metaUrl}/oauth");
                client.Timeout = -1;
                client.Authenticator = new HttpBasicAuthenticator(username, password);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("application/json", "{\"grant_type\":\"client_credentials\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                MetaAuthenticatorResponse reply = JsonConvert.DeserializeObject<MetaAuthenticatorResponse>(response.Content.ToString());
                autorizationtoken = reply.access_token;
            } 
            catch (Exception ex) {
                _log.Logger($"An error occurred during authentication with Meta server in MetaAuthentication on MetaIntegration. Details: {ex.Message}", "ERROR");
            }
           return autorizationtoken;
        }
        private MetaVerificationResponse CreateVerification(MetaVerificationRequestObject mvr)
        {
            MetaVerificationResponse verifyres = new MetaVerificationResponse();
            string token = MetaAuthentication();
            try
            {
                var client = new RestClient($"{metaUrl}/v2/verifications");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {token}");
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(mvr);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                verifyres = JsonConvert.DeserializeObject<MetaVerificationResponse>(response.Content.ToString());
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred during meta verification creation in CreateVerification on MetaIntegration. Details: {ex.Message}", "ERROR");
            }
            return verifyres;
        }

        public IdentityResponse IndentityVerification(MetaIdentityVerificationRequestObject miv)
        {
            IdentityResponse verifyres = new IdentityResponse();            
            string token = MetaAuthentication();
            var cres = CreateVerification(new MetaVerificationRequestObject() {flowId = flowId, metadata = new Metadata() { user = miv.CustomerName, id = miv.CustomerAccountNubmer } });
            try
            {
                if(miv.DocumentFront.Length > 0 && miv.DocumentBack.Length > 0 && miv.Liveness.FileName != null)
                {
                    if(!Directory.Exists(_env.WebRootPath +"\\UploadedFiles"))
                    {
                        Directory.CreateDirectory(_env.WebRootPath +"\\UploadedFiles");
                    }
                    using(FileStream fileStream = File.Create(_env.WebRootPath + "\\UploadedFiles"+miv.DocumentFront.FileName))
                    {
                        miv.DocumentFront.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    using (FileStream fileStream = File.Create(_env.WebRootPath + "\\UploadedFiles" + miv.DocumentBack.FileName))
                    {
                        miv.DocumentFront.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    using (FileStream fileStream = File.Create(_env.WebRootPath + "\\UploadedFiles" + miv.Liveness.FileName))
                    {
                        miv.DocumentFront.CopyTo(fileStream);
                        fileStream.Flush();
                    }

                    string requestinput = JsonConvert.SerializeObject(miv.inputs);
                    var client = new RestClient($"{metaUrl}/v2/identities/{cres.identity}/send-input");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "multipart/form-data");
                    request.AddHeader("Authorization", $"Bearer {token}");
                    request.AddParameter("inputs", requestinput);
                    request.AddFile("document", _env.WebRootPath + "\\UploadedFiles" + miv.DocumentFront.FileName);
                    request.AddFile("document", _env.WebRootPath + "\\UploadedFiles" + miv.DocumentBack.FileName);
                    request.AddFile("video", _env.WebRootPath + "\\UploadedFiles" + miv.Liveness.FileName);
                    IRestResponse response = client.Execute(request);
                    if(response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        verifyres = new IdentityResponse() { ResponseCode = "00", Responsemessage = "Document Upload was successful" };
                    }
                    else
                    {
                        verifyres = new IdentityResponse() { ResponseCode = "01", Responsemessage = "Document Upload Failed! please ensure that image are clear and has a minimum size of W = 600px, H = 450px, Video should not be more than 50MB " };
                    }
                }
                else
                {
                    verifyres = new IdentityResponse() { ResponseCode = "11", Responsemessage = "Please upload valid files" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred during meta verification creation in IndentityVerification on MetaIntegration. Details: {ex.Message}", "ERROR");
            }
            return verifyres;
        }
    }
}
