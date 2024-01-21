using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class LoanApplication : ILoanApp
    {
        IConfiguration _config;
        ILogs _log;
        ICustomer _cs;
        IProvenir prd;
        ILoanService ln;       
        ILoanPro _lrp;
        IBone bi;
        IBoneLoan blp;
        IMailer _mailer;
        public LoanApplication(IConfiguration config, ICustomer cs, IProvenir _prd, ILoanService _ln, ILoanPro lrp, IBone _bi, IBoneLoan _blp, IMailer mailer, ILogs log)

        {
            _config = config;
            _log = log;
            _cs = cs;
            prd = _prd;
            ln = _ln;           
            _lrp = lrp;
            bi = _bi;
            blp = _blp;
            _mailer = mailer;
        }        
       
        //public async Task<LoanProccessingResponseObject> ProccessLoanApplication(ProvenirCreditDecisionRequestObject pcd)
        //{
        //    LoanProccessingResponseObject gcc = new LoanProccessingResponseObject();
        //    int creditscorepassMark = int.Parse(_config.GetSection("CreditScorePassmark").Value);
        //    try
        //    {
        //        long response = await _cs.SaveOrUdateCusomerLoanProcessingDetails(new CustomerDetail()
        //        {
        //            CustomerId = pcd.clientId,
        //            FirstName = pcd.details.firstname,
        //            LastName = pcd.details.lastname,
        //            MaritalStatus = pcd.details.maritalStatus,
        //            Gender = pcd.details.gender,
        //            DateOfBirth = pcd.details.dateOfBirth,
        //            Street = pcd.location.street,
        //            City = pcd.location.city,
        //            State = pcd.location.state,
        //            PostalCode = pcd.location.postalCode,
        //            Country = pcd.location.country,
        //            KYC_score = pcd.@internal.KYC_score,
                                      
        //            EmploymentDuration = int.Parse(pcd.employment.duration),
        //            Industry = pcd.employment.industry,
        //            LevelOfEducation = pcd.details.levelOfEducation,
        //            Income = decimal.Parse(pcd.employment.income),
        //            Type = pcd.clientType,
        //            ResidenceStatus = pcd.location.residenceStatus,
        //            IncomeVerified = pcd.employment.incomeVerified,
        //            EmploymentType = pcd.employment.type
        //        });
        //        if (response > 0)
        //        {
        //            CreditBureauSource crcReport = new CreditBureauSource();
        //            CreditBureauSource fcReport = new CreditBureauSource();
        //            GetScoreResponseObject custrep = new GetScoreResponseObject();
        //            int fcs = 0; int totalAvgScore = 0;
        //            string remark = "Failed";
        //            GetIdentityResponseObject gir = _all.GetCustomerIdentity(pcd.bureau.BVN);
        //            if (gir != null && gir.customerId != null)
        //            {
        //                CreditBureauResult rs = await _cs.GetMostRecentCreditBureauScore(pcd.clientId);
        //                if (rs.CreditRemark != null)
        //                {
        //                    custrep = JsonConvert.DeserializeObject<GetScoreResponseObject>(rs.CreditBureauDetails);
        //                }
        //                else
        //                {
        //                    custrep = _all.GetCustomerCreditScore(new GetScoreRequestObject() { fields = "score", customerId = gir.customerId });
        //                    fcs = custrep.sources[1].data.score * 1000 / 850;
        //                    totalAvgScore = fcs + custrep.sources[0].data.score / 2;
        //                    if (totalAvgScore > creditscorepassMark) { remark = "Passed"; }
        //                    await _cs.SaveCreditBureau(new CreditBureauResult() { Date = DateTime.Now, CustomerId = pcd.clientId, CreditBureauDetails = JsonConvert.SerializeObject(custrep.sources), CreditScore = totalAvgScore, CreditRemark = remark });
        //                }
        //                if (totalAvgScore >= creditscorepassMark)
        //                {
        //                    foreach (CreditScoreSource css in custrep.sources)
        //                    {
        //                        if (css.source == "CRC")
        //                        {
        //                            crcReport.source = css.source;
        //                            crcReport.score = css.data.score;
        //                            crcReport.rating = css.data.rating;
        //                            crcReport.reasons = css.data.reasons;
        //                        }
        //                        if (css.source == "FIRST_CENTRAL")
        //                        {
        //                            fcReport.source = css.source;
        //                            fcReport.score = css.data.score;
        //                            fcReport.rating = css.data.rating;
        //                            fcReport.reasons = css.data.reasons;
        //                        }
        //                    }
        //                    pcd.bureau.sources[0] = crcReport;
        //                    pcd.bureau.sources[1] = fcReport;
        //                    string requestId = "SNL/" + DateTime.Now.ToString("ddMMyyyyhhmmss");
        //                    ProvenirCreditDecisionResponseObject pd = await _lrp.GetLoanOffersFromProvenir(pcd);
        //                    pd.ReferenceId = requestId;
        //                    pd.EmploymentType = pcd.employment.type;
        //                    await prd.SaveProvenirData(pd);
        //                    return new LoanProccessingResponseObject()
        //                    {
        //                        ResponseCode = "00",
        //                        ResponseMessage = "Loan Proccessing Completed",
        //                        LoanRequestDecision = pd.overallDecision,
        //                        LoanOffers = pd,
        //                        RerenceId = requestId
        //                    };
        //                }
        //                else
        //                {
        //                    return new LoanProccessingResponseObject()
        //                    {
        //                        ResponseCode = "01",
        //                        ResponseMessage = "FAILED! Customer Credit Score on either of the credit Bureau was too Low!",
        //                        LoanRequestDecision = "DENIED"
        //                    };
        //                }
        //            }
        //            else
        //            {
        //                gcc = new LoanProccessingResponseObject()
        //                {
        //                    ResponseCode = "02",
        //                    ResponseMessage = "FAILED! Could not validate customer Identity on Credit Score API server!",
        //                    LoanRequestDecision = "DENIED"
        //                };
        //            }
        //        }
        //        else
        //        {
        //            gcc = new LoanProccessingResponseObject()
        //            {
        //                ResponseCode = "02",
        //                ResponseMessage = "FAILED! Save Customer Application details to Database!",
        //                LoanRequestDecision = "INCOMPLETE"
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gcc = new LoanProccessingResponseObject()
        //        {
        //            ResponseCode = "011",
        //            ResponseMessage = ex.Message,
        //            LoanRequestDecision = "DENIED"
        //        };
        //        _log.Logger("An error occurred on the ProccessCustomerLoanRequest method! Details: " + ex.Message, "ERROR");
        //    }
        //    return gcc;
        //}

        public async Task<LoanRequestResponse> BookCustomerLoan(LoanRequestDTO lro)
        {
            try
            {
                if (bi.AddCardValidation(lro.CustomerID))
                {
                    string productCode = _config.GetSection("NanoLoanProductCode").Value;
                    LoanProcessingData lp = await prd.GetCustomerProvenirData(lro.ReferenceID);
                    decimal intRate = lro.Rate;
                    decimal interestAmt = intRate / 100 * lro.Amount;
                    decimal amountpayable = lro.Amount + interestAmt;
                    decimal bankoneAmt = lro.Amount * 100;
                    BankOneLoanRequestResponse response = await blp.CreateLoanOnBankOne(new LoanRequestObject()
                    {
                        CustomerID = lro.CustomerID,
                        Amount = (int)lro.Amount,
                        Tenure = lro.Tenure,
                        LinkedAccountNumber = lro.LinkedAccountNumber,
                        InterestRate = (int)intRate,
                        LoanProductCode = productCode,
                        ComputationMode = 0,
                        Moratorium = 0,
                        InterestAccrualCommencementDate = DateTime.Now,
                        TransactionTrackingRef = lro.ReferenceID
                    });

                    if (response.IsSuccessful)
                    {
                        return new LoanRequestResponse() { ResponseCode = "00", ResponseMessage = response.Message };
                    }
                    else
                        return new LoanRequestResponse() { ResponseCode = "02", ResponseMessage = "Could not create this Loan! Details: " + response.Message };
                }
                else
                {
                    return new LoanRequestResponse() { ResponseCode = "02", ResponseMessage = "Please Add your Card!" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the ProcessLoanRequest method! Details: " + ex.Message, "ERROR");
                return new LoanRequestResponse() { ResponseCode = "03", ResponseMessage = "An error occurred! Details: " + ex.Message };
            }
        }

        public async Task<LoanRequestResponse> ProcessNanoLoan(LoanRequestDTO lro)
        {
            try
            {
                string productCode = _config.GetSection("NanoLoanProductCode").Value;
                LoanProcessingData lp = await prd.GetCustomerProvenirData(lro.ReferenceID);
                decimal intRate = lro.Rate;
                decimal interestAmt = intRate / 100 * lro.Amount;
                decimal amountpayable = lro.Amount + interestAmt;
                //decimal bankoneAmt = lro.Amount * 100;
                if (await ln.IsReferenceNumberValid(lro.ReferenceID))
                {
                    long response = await ln.CreateNewLoan(new NanoLoan()
                    {
                        Date = DateTime.Now,
                        LoanReferenceId = lro.ReferenceID,
                        CustomerId = lro.CustomerID,
                        CustomerName = lro.CustomerName,
                        PhoneNumber = lro.PhoneNumber,
                        Email = lro.Email,
                        LoanAmount = lro.Amount,
                        Tenure = lro.Tenure,
                        SofriAccountNumber = lro.LinkedAccountNumber,
                        InterestRate = (int)intRate,
                        InterestAmount = interestAmt,
                        TotalPayable = amountpayable,
                        LoanBalance = amountpayable,
                        NextRepaymentDate = NextPaymentDate(lro.Tenure),
                        RepaymentStartDate = DateTime.Now.AddDays(15),
                        RepaymentEndDate = DateTime.Now.AddDays(lro.Tenure),
                        IsOverDue = false,
                        Remark = "PROCESSING COMPLETED, PENDING DISBURSEMENT",
                        Status = "PENDING DISBURSEMENT",
                        ValidTill = DateTime.Now.AddHours(1)
                    });
                    if (response > 0)
                    {
                        string rely = LoanActivation(lro, lro.Amount);
                        return new LoanRequestResponse() { ResponseCode = "00", ResponseMessage = rely };
                    }
                    else
                    {
                        return new LoanRequestResponse() { ResponseCode = "01", ResponseMessage = "Could not Complete Loan processing!" };
                    }
                }
                else
                {
                    return new LoanRequestResponse() { ResponseCode = "03", ResponseMessage = "Could not process this request! Reason: Duplicate Reference Id is not allowed" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on the ProcessNanoLoan method! Details: " + ex.Message, "ERROR");
                return new LoanRequestResponse()
                {
                    ResponseCode = "02",
                    ResponseMessage = ex.Message
                };
            }
        }

        public async Task<LoanRequestResponse> RetryNanoLoanDisbursement()
        {
            try
            {
                _log.ServiceMsg("Starting Loan Disbursement Retrial at ( " + DateTime.Now + ")!", "INFO");
                IEnumerable<NanoLoan> dueloans = ln.GetAllUnDisburseLoans();
                _log.ServiceMsg("Total failed Disbursement found ( " + dueloans.Count() + ")!", "INFO");
                foreach (NanoLoan n in dueloans)
                {
                    _log.ServiceMsg("Attempting disbursement and Activation of loan with ReferenceID ( " + n.LoanReferenceId + ")!", "INFO");
                     LoanActivation(new LoanRequestDTO()
                    {
                        ReferenceID = n.LoanReferenceId,
                        CustomerID = n.CustomerId,
                        CustomerName = n.CustomerName,
                        PhoneNumber = n.PhoneNumber,
                        Email = n.Email,
                        Amount = n.LoanAmount,
                        Rate = n.InterestRate,
                        Tenure = n.Tenure,
                        LinkedAccountNumber = n.SofriAccountNumber,
                    }, n.LoanAmount);
                }
                _log.ServiceMsg("Loan Disbursement Retried at ( " + DateTime.Now + ") Completed Successfully!", "INFO");
            }
            catch (Exception ex)
            {
               _log.Logger("Error occurred on RetryNanoLoanDisbursement service. Details: " + ex.Message, "ERROR");
            }
            return null;
        }

        public string LoanActivation(LoanRequestDTO lro, decimal loanAmount)
        {
            string prodcode = _config.GetSection("NanoloanProductCode").Value;
            int cycle = int.Parse(_config.GetSection("BankOneInterestCycle").Value);
            try
            {
                BankOneLoanCreationResponse rep = bi.CreateLoanOnBankOne(new BankOneLoanCreationRequest()
                {
                    CustomerID = lro.CustomerID,
                    LinkedAccountNumber = lro.LinkedAccountNumber,
                    CollateralDetails = null,
                    CollateralType = null,
                    LoanProductCode = prodcode,
                    Amount = int.Parse(lro.Amount.ToString()),
                    TransactionTrackingRef = lro.ReferenceID,
                    Tenure = lro.Tenure,
                    InterestRate = int.Parse(lro.Rate.ToString())*cycle,
                    ComputationMode = 0,
                    InterestAccrualCommencementDate = DateTime.Today,
                    InterestPaymentFrequency = 1,
                    Moratorium = 0,
                    PrincipalPaymentFrequency = 1
                }) ;
                if (rep.IsSuccessful)
                {
                    long resp = ln.UpdateLoanStatus(lro.ReferenceID, "ACTIVE", rep.TransactionTrackingRef.ToString(), "PROCESSING COMPLETED! DISBURSED SUCCESSFULLY!");
                    if (resp > 0)
                    {
                        long res = ln.CreateRepaymentSchedule(lro.ReferenceID, lro.Tenure, loanAmount, lro.Rate);
                        if (res > 0)
                        {
                            return rep.Message;
                        }
                        else
                        {
                            return "Loan Disbursed successfully, but could not create repayment Schedule!";
                        }
                    }
                    else
                    {
                        return "Loan Disbursed successfully, but loan stataus update failed";
                    }
                }
                else
                {
                    _log.Logger($"Customer with Id: {lro.CustomerID} loan disbursement on bankOne Failed! Loan Details {JsonConvert.SerializeObject(lro)}","DISBURSEMENT");
                    _mailer.SendEmail(new DTO.MailObject() { CustomerId = lro.CustomerID, FirstName = lro.CustomerName, LoanAmount = lro.Amount.ToString("#,##0.00") });
                    ln.UpdateLoanStatus(lro.ReferenceID, "PENDING", rep.TransactionTrackingRef.ToString(), "PROCESSING COMPLETED, PENDING DISBURSEMENT");
                    return "Your loan will be disbursed shortly!";
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on LoanActivation Method! Detials: {ex.Message}","ERROR");
                return ex.Message;
            }
        }

        decimal getFee(decimal Amount)
        {
            decimal feeRate = decimal.Parse(_config.GetSection("loanFeeRate").Value);
            decimal intamt = feeRate / 100 * Amount;
            return intamt * 100;
        }
        string NextPaymentDate(int tenor)
        {
            if (tenor <= 30)
            {
                return DateTime.Now.AddDays(tenor).ToShortDateString();
            }
            else
            {
                return DateTime.Now.AddDays(30).ToShortDateString();
            }
        }
        int GetPaymentPeriod(int tenor)
        {
            int p = 0;
            if (tenor >= 30)
            {
                p = 1;
            }
            if (tenor > 30 || tenor <= 60)
            {
                p = 2;
            }
            if (tenor > 60 || tenor <= 90)
            {
                p = 3;
            }
            return p;
        }
    }
}
