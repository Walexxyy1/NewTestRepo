using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Linq;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class ValidateUser : IValidate
    {
        ApiauthorisationContext _ctx;
        ILogs _log;
        public ValidateUser(ApiauthorisationContext ctx, ILogs log)
        {
            _log = log; 
            _ctx = ctx;
        }
        
        public bool getUserAuth(string apiKey)
        {
            Authorised details = new Authorised();
            try
            {
               // string query = "SELECT * FROM Authorised WHERE AppKey=@Key";
                details = _ctx.Authoriseds.Where(x => x.AppKey == apiKey).FirstOrDefault(); //repo.Query<Authorised>(query, new { Key = apiKey });
                if (details != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
              _log.Logger($"An error occurred on getUserAuth method in ValidateUser. Details: {ex.Message}", "Error");
            }
            return false;
        }

        public bool getAuthorisedUser(string apikey, string appid)
        {
            Authorised details = new Authorised();
            try
            {
               
                details = _ctx.Authoriseds.Where(x => x.AppKey == apikey && x.AppId == appid).FirstOrDefault();
                if (details != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on getAuthorisedUser method in ValidateUser. Details: {ex.Message}", "Error");
            }
            return false;
        }
       
    }
}
