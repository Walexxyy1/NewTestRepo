using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
   public class CreditBureauService : ICBScore
    {
        public ICRC _crc { get; }

        private readonly ICRegistry _cry;
        ILogs _log;

        public CreditBureauService(ICRC crc, ICRegistry cry, ILogs log)
        {
            _log = log;
            _crc = crc;
            _cry = cry;
        }
        public async Task<decimal> GetCreditScore(CRegisteryCreditScoreRequestDTO cs)
        {
            int avgCreditScore = 0;
            try
            {
                Credit202Response registry = await _cry.GetCustomerCreditScoreAsync(cs);
                CRCSCResponse crcres = await _crc.GetCRCCreditScoreAsync(cs.BVN, cs.CustomerId);
                int creditregScore = 0;
                int crcScore = 0;
                if (crcres.ScoreDetails.ConsumerHitResponse != null) 
                { 
                    crcScore = crcres.ScoreDetails.ConsumerHitResponse.BODY.CREDIT_SCORE_DETAILS.CREDIT_SCORE_SUMMARY.CREDIT_SCORE; 
                }
                if(registry.SMARTScores[0] != null)
                {
                     creditregScore = registry.SMARTScores[0].GenericScore;
                }
                if (creditregScore == 0)
                {
                    avgCreditScore = crcScore;
                }
                else if (crcScore == 0)
                {
                    avgCreditScore = creditregScore;
                }
                else
                {
                    int totalscore = creditregScore + crcScore;
                    avgCreditScore = totalscore / 2;

                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on the CreditBureauService GetCreditScore! Details: {ex.Message}", "ERROR");
            }
            return avgCreditScore;
        }
       
    }
}
