using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.AllaweeObjects;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonoAccount = BANKSOFRI_LOAN.DATALAYER.Models.MonoAccount;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class CRLogic : ICrlogic
    {
        IConfiguration _config;
        ILogs _log;
        ILoanPro _lrp;       
        IVerifyBvn _vfy;
        ICustomer _cs;
        IProvenir _prv;
        ICBScore _cbscore;
        private readonly IMono _m;
        private readonly IMoint _mt;
        private readonly ILoanService _loans;

        public CRLogic(IConfiguration config,ILogs log, ILoanPro lrp,  IVerifyBvn vfy, ICustomer cs, IProvenir prv, ICBScore cbscore, IMono m, IMoint mt, ILoanService loan)
        {
            _config = config;
            _log = log;
            _lrp = lrp;            
            _vfy = vfy;
            _cs = cs;
            _prv = prv;
            _cbscore = cbscore;
            _m = m;
            _mt = mt;
            _loans = loan;
        }       
       public async Task<double> TestORRCalculation(NanoLoanRequestDTO cd)
        {
            double orr = await _cs.GetCustomerORR(cd.CustomerId);
            return orr;
        }

        public async Task<LoanProccessingResponseObject> ProccessCustomerLoanRequest(NanoLoanRequestDTO cd)
        {
            LoanProccessingResponseObject lpresp = new LoanProccessingResponseObject();
            if (await _cs.IsAccountValid(cd.CustomerId))
            {
                if (!await _cs.HasActiveNanoLoan(cd.CustomerId))
                {
                    if (!await _cs.HasPendingNanoLoan(cd.CustomerId))
                    {
                        _log.Logger($"Loan Request for customer Id:{cd.CustomerId} Name:{cd.FirstName}{cd.LastName}. request details {JsonConvert.SerializeObject(cd)}", "INFO");
                        int creditscorepassMark = int.Parse(_config.GetSection("CreditScorePassmark").Value);
                        string prodCode = _config.GetSection("ProvenirProductCode").Value;
                        try
                        {
                            var ctype = await _cs.GetCustomerType(cd.CustomerId);
                            cd.Type = ctype.CustomerType;
                            cd.EmploymentDuration = int.Parse(employmentDuration(cd.startDate).ToString());
                            long response = await _cs.SaveOrUdateCusomerLoanProcessingDetails(cd);
                            // MonoAccount monodetail = await _m.GetCustomerMonoAccount(cd.CustomerId);
                            if (response > 0)
                            {
                                string dob = DateTime.Parse(cd.DateOfBirth).ToString("dd-MM-yyyy");
                                VerifymeBVNResponse bvndetails = _vfy.ValidateBVNOnVerifyMe(new VerifyMeBVNValidationRequestObject() { CustomeId = cd.CustomerId, BVN = cd.BVN, firstname = cd.FirstName, lastname = cd.LastName, dob = dob });
                                if (bvndetails.data != null)
                                {
                                    string unwantedBVNInstitutionCode = _config.GetSection("unwantedBVNInstitutionCode").Value;
                                    if (bvndetails.data.enrollmentBank != unwantedBVNInstitutionCode)
                                    {
                                        decimal totalAvgScore = 0;
                                        decimal totalscore = await _cbscore.GetCreditScore(new CRegisteryCreditScoreRequestDTO() { CustomerId = cd.CustomerId, BVN = cd.BVN, CreditScoreEnquiryReason = "KYCCheck", EnquiryLoanAmount = cd.LoanAmount, RetroDate = DateTime.Today.ToShortDateString() });
                                        if (totalscore > 0)
                                        {
                                            if (totalscore >= creditscorepassMark)
                                            {
                                                totalAvgScore = totalscore;
                                            }
                                            else
                                            {
                                                return new LoanProccessingResponseObject()
                                                {
                                                    ResponseCode = "002",
                                                    ResponseMessage = "FAILED! Customer Credit Score on either of the credit Bureau was too Low!",
                                                    LoanRequestDecision = "DECLINED"
                                                };
                                            }
                                        }
                                        else
                                        {
                                            totalAvgScore = decimal.Parse(_config.GetSection("CreditScoreLeastScore").Value);
                                        }
                                        string loanRequid = "SNL/" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                                        var ct = await _cs.GetCustomerType(cd.CustomerId);
                                        double orr = await _cs.GetCustomerORR(cd.CustomerId);
                                        //DateTime birtday = DateTime.Parse(cd.DateOfBirth);
                                        long appid = await _cs.GetApplicationId();
                                        ProvenirCreditDecisionRequestObject pcd = new ProvenirCreditDecisionRequestObject()
                                        {
                                            clientId = cd.CustomerId,
                                            location = new Location() { street = cd.Street, city = cd.City, state = cd.State, postalCode = cd.PostalCode, country = "NGA", residenceStatus = cd.ResidenceStatus },
                                            details = new Details() { firstname = cd.FirstName, lastname = cd.LastName, gender = cd.Gender, dateOfBirth = DateTime.Parse(cd.DateOfBirth).ToString("yyyy-MM-dd"), maritalStatus = cd.MaritalStatus, levelOfEducation = cd.LevelOfEducation },
                                            employment = new Employment() { employer_name = cd.EmployerName, industry = cd.Industry, duration = employmentDuration(cd.startDate).ToString(), type = cd.EmploymentType, income = cd.Income.ToString(), incomeVerified = false },
                                            contact = new Contact() { email = cd.Email, phone = FormatPhoneNumber(cd.Phone) },
                                            clientType = ct.CustomerType,
                                            applicationId = appid.ToString(),
                                            desiredAmount = cd.LoanAmount.ToString(),
                                            identity = new Identity() { idVerified = true },
                                            loanPurpose = cd.LoanPurpose,
                                            loanNumber = ct.LoanNumber.ToString(),
                                            @internal = new InternalProc() { KYC_score = "Level 3", ontime_repayment_rate = orr.ToString() },
                                            bureau = new Bureau() { BVN = cd.BVN, Count_AccountStatus_Written_Off = "0", Count_AccountStatus_Performing = "0", Count_AccountStatus_Delinquent_30_over_60_days = "", Balance_Total = "", Score = totalAvgScore.ToString() },
                                            productCode = prodCode
                                        };

                                        ProvenirCreditDecisionResponseObject pd = await _lrp.GetLoanOffersFromProvenir(pcd);
                                        await _prv.SaveProvenirData(pd);
                                        if (pd.overallDecision == "Accept")
                                        {
                                            pd.offers = await CalculateLoanOffersAsync(pd.offers, cd.LoanAmount, cd.CustomerId);
                                            lpresp = new LoanProccessingResponseObject()
                                            {
                                                ResponseCode = "00",
                                                ResponseMessage = "SUCCESS! Loan Request was successful!",
                                                LoanRequestDecision = pd.overallDecision,
                                                LoanOffers = pd,
                                                RerenceId = loanRequid,
                                                CardStatus = await _cs.GetCustomerCardStatus(cd.Email)
                                            };
                                        }
                                        else
                                        {
                                            lpresp = new LoanProccessingResponseObject()
                                            {
                                                ResponseCode = "011",
                                                ResponseMessage = "FAILED! We are not able to Offer you a Loan at the moment!",
                                                LoanRequestDecision = pd.overallDecision,
                                                LoanOffers = pd,
                                                RerenceId = loanRequid
                                            };
                                        }
                                        await _cs.SaveProvenirRequest(new NanoLoanRequest() { Date = DateTime.Now, CustomerId = cd.CustomerId, FirstName = cd.FirstName, LastName = cd.LastName, Email = cd.Email, PhoneNumber = cd.Phone, Gender = cd.Gender, EmploymentType = cd.EmploymentType, Income = cd.Income, LoanAmount = cd.LoanAmount });
                                    }
                                    else
                                    {
                                        lpresp = new LoanProccessingResponseObject()
                                        {
                                            ResponseCode = "004",
                                            ResponseMessage = "FAILED! BVN enrollment institution is INVALID!",
                                            LoanRequestDecision = "DECLINED"
                                        };
                                    }
                                }
                                else
                                {
                                    lpresp = new LoanProccessingResponseObject()
                                    {
                                        ResponseCode = "005",
                                        ResponseMessage = "INVALID BVN! BVN details not Found!",
                                        LoanRequestDecision = "DECLINED"
                                    };
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.Logger("An error occurred on the ProccessCustomerLoanRequest method! Details: " + ex.Message, "ERROR");
                        }
                    }
                    else
                    {
                        lpresp = new LoanProccessingResponseObject() { ResponseCode = "010", ResponseMessage = "You currently have a loan pending disbursement", RerenceId = "0000" };
                    }
                }
                else
                {
                    lpresp = new LoanProccessingResponseObject() { ResponseCode = "011", ResponseMessage = "This customer has an ACTIVE LOAN", RerenceId = "0000" };
                }
            }
            else
            {
                lpresp = new LoanProccessingResponseObject() { ResponseCode = "012", ResponseMessage = "Account needs to be upgraded first!", RerenceId = "0000" };
            }
            return lpresp;
        }

        string FormatPhoneNumber(string phoneNo)
        {
            string modifiedpone = null;
            if(phoneNo.StartsWith("234") || phoneNo.StartsWith("+234"))
            {
                modifiedpone = phoneNo;
            }
            else
            {
                modifiedpone = phoneNo.Remove(0, 1).Insert(0,"234");
            }
            return modifiedpone;
        }
        double employmentDuration(string sd)
        {
           DateTime startdate = DateTime.Parse(sd);
            int year = DateTime.Now.Year - startdate.Year;
            return year;
        }
        
        async Task<List<Offer>> CalculateLoanOffersAsync(List<Offer> provenirOffers, decimal loanAmount, string customerId)
        {
            List<Offer> loanOffers = new List<Offer>();
            decimal maxi = decimal.Parse(provenirOffers.Max(x => x.maxAmount));
            decimal amount = 0;
            if (maxi >= loanAmount)
            {
                amount = loanAmount;
            }            
            else
            {
                amount = maxi;
            }
            foreach (Offer o in provenirOffers)
            {               
              loanOffers.Add(new Offer() { maxAmount = Math.Ceiling(amount).ToString(), rate = o.rate, tenor = o.tenor });                
            }
            
            await _lrp.PersistCustomerOffers(new CustomerOffer() { Date = DateTime.Now, CustomerId = customerId, Offers = System.Text.Json.JsonSerializer.Serialize(loanOffers), Status = "ACCEPTED" });
            return loanOffers;
        }
    
    }
}
