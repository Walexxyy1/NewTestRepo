using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
   public class OtherSofriLoan : IOthers
    {
        IBone b;       
        IConfiguration _config;
        ILogs _log;
        ApiauthorisationContext _ctx;
        public OtherSofriLoan(IBone _b, IConfiguration config, ILogs log, ApiauthorisationContext ctx)
        {
            b = _b;           
            _config = config;
            _log = log;
            _ctx = ctx;
        }
        public async Task<ResponseObject> SaveOtherLoanApplication(OtherLoansDTO ol)
        {
            long response = 0;
            ResponseObject rs = new ResponseObject();
            try
            {
                decimal minAmount = decimal.Parse(_config.GetSection("OtherLoanMinAmount").Value);
                if (ol.LoanAmount < minAmount)
                {
                    return new ResponseObject() { ResponseCode = "03", ResponseMessage = "INVALID LOAN AMOUNT! Minimum value for this loan amount is: " + minAmount.ToString() };
                }
                else
                {
                    CustomerDetailsDTO cd = await b.GetByCustomerID(ol.CustomerId);
                    Account accountNumber = cd.Accounts.Where(x => x.NUBAN == ol.AccountNumber).FirstOrDefault();
                    if (cd != null && accountNumber != null)
                    {
                          await _ctx.OtherLoans.AddAsync(new OtherLoan() { Date = DateTime.Now, CustomerId = ol.CustomerId, CustomerName = cd.name, Bvn = cd.BVN, PhoneNumber = cd.phoneNumber, Email = cd.email, LoanAmount = ol.LoanAmount, LoanPurpose = ol.LoanPurpose, LoanType = ol.LoanType });
                        response = await _ctx.SaveChangesAsync();
                        // response = await r.InsertAsync<OtherLoan>(new OtherLoan() { Date = DateTime.Now, CustomerId = ol.CustomerId, CustomerName = cd.name, BVN = cd.BVN, PhoneNumber = cd.phoneNumber, Email = cd.email, LoanAmount = ol.LoanAmount, LoanPurpose = ol.LoanPurpose, LoanType = ol.LoanType });
                        if (response > 0)
                        {
                            rs = new ResponseObject() { ResponseCode = "00", ResponseMessage = "SUCCESS! Loan Request Submitted successfully!" };
                        }
                        else
                        {
                            rs = new ResponseObject() { ResponseCode = "01", ResponseMessage = "FAILED! Could not submit Loan Request!" };
                        }
                    }
                    else
                    {
                        rs = new ResponseObject() { ResponseCode = "02", ResponseMessage = "FAILED! Invalid Customer Id or Account Number!" };
                    }
                }
            }
            catch (Exception ex)
            {
              _log.Logger("An error occured on SaveOtherLoanApplication method! Details: " + ex.Message, "Error");
            }
            return rs;
        }

        public async Task<GetOtherLoansResponse> FetchOtherLoans()
        {
            GetOtherLoansResponse go = new GetOtherLoansResponse();
            List<OtherLoan> o = new List<OtherLoan>();
            try
            {
                o = await _ctx.OtherLoans.ToListAsync();
                if(o.Count() > 0)
                {
                    go = new GetOtherLoansResponse() { ResponseCode = "00", ResponseMessage = "Other Loan Records fetched successfully", loans = o };
                }
                else
                {
                    go = new GetOtherLoansResponse() { ResponseCode = "01", ResponseMessage = "Other other loan Record was found!" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on FetchOtherLoans method! Details: " + ex.Message, "Error");
            }
            return go;
        }

        public async Task<GetOtherLoansResponse> FetchCustomerOtherLoans(string customerId)
        {
            GetOtherLoansResponse go = new GetOtherLoansResponse();
            List<OtherLoan> o = new List<OtherLoan>();
            try
            {
                o = await _ctx.OtherLoans.Where(x => x.CustomerId == customerId).ToListAsync(); //r.Query<List<OtherLoan>>("SELECT * FROM OtherLoans WHERE CustomerId= @custId", new { custId = customerId });
                if (o != null)
                {
                    go = new GetOtherLoansResponse() { ResponseCode = "00", ResponseMessage = "Other Loan Records fetched successfully", loans = o };
                }
                else
                {
                    go = new GetOtherLoansResponse() { ResponseCode = "01", ResponseMessage = "Other other loan Record was found for this customer!" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on FetchCustomerOtherLoans method! Details: " + ex.Message, "Error");
            }
            return go;
        }
    }
}
