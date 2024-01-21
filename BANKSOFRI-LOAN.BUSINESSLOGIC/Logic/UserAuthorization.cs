using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class UserAuthorization : IUserAuth
    {
        IConfiguration _config;
        ILogs _log;
        DOMAINOBJECTS.DBObjects.ApiauthorisationContext _ctx;
        public UserAuthorization(IConfiguration config, ILogs log, ApiauthorisationContext ctx)
        {
            _config = config;
            _log = log;
            _ctx = ctx; 
        }
        //AppLogger log = new AppLogger();
        private static Random random = new Random();
        Repository repo = new Repository();
        public async Task<string> GetAuthToken(AuthDTO uat)
        {
            string authtoken = null;
            try
            {
                authtoken = GenerateToken(15);
                int minutes = int.Parse(_config.GetSection("TokenValidMinutes").Value);
                await _ctx.UserAuthentications.AddAsync(new UserAuthentication() { UserEmail = uat.Email, UserId = uat.UserId, TokenCode = authtoken, TimeIssued = DateTime.Now, ExpirationTime = DateTime.Now.AddMinutes(minutes), Status = "ACTIVE" });
                long resp = await _ctx.SaveChangesAsync();
                if (resp > 0)
                {
                    _log.Logger("Request for token recieved for user with details as follows email: " + uat.Email + " And User Id:" + uat.UserId + ". request token was sent successfully", "Info");
                    return authtoken;
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on {ex.Message}", "Error");
            }
            return authtoken;
        }

        public async Task<string> UserAuthTokenValidation(AuthValidationDTO avd)
        {
            //bool isValid = false;
            //string ud = null;
            try
            {
               // string query = "SELECT * FROM UserAuthentication WHERE UserEmail=@umail AND TokenCode=@tcode";
                UserAuthentication resp = _ctx.UserAuthentications.Where(x => x.UserEmail == avd.Email && x.TokenCode == avd.Token).FirstOrDefault(); //repo.Query<UserAuthentication>(query, new { umail = avd.Email, tcode = avd.Token });
                if (resp == null)
                {
                    return null;
                }
                if (resp.Status == "ACTIVE" && resp.ExpirationTime > DateTime.Now)
                {
                    long repmessage = await UpdateUserTokenStatus(resp);
                    if (repmessage > 0)
                    {
                        _log.Logger("Token validation request recieved for user with details " + avd.Email + ". Validation request was successful", "Info");
                        return resp.UserId;
                    }
                    else
                        return null;
                }
                else
                   _log.Logger("Token validation request recieved for user with details " + avd.Email + ". Validation request Failed. Token details not found!", "Info");
            }
            catch (Exception ex)
            {
               _log.Logger(ex.Message, "Error");
            }
            return null;
        }

        public async Task<long> UpdateUserTokenStatus(UserAuthentication au)
        {
            long resp = 0;
            try
            {
                au.Status = "USED";
                au.TimeUsed = DateTime.Now;
                _ctx.Update(au);
                resp = await _ctx.SaveChangesAsync();
               // resp = await repo.UpdateAsync<UserAuthentication>(au);
            }
            catch (Exception ex)
            {
                _log.Logger(ex.Message, "Error");
            }
            return resp;
        }
        public static string GenerateToken(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
