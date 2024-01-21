using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
   public class Repayment : IRepay
    {
        private readonly IOtherloans _io;
        private readonly ICollect _ic;

        public Repayment(IOtherloans io, ICollect ic)
        {
            _io = io;
            _ic = ic;
        }
        public void LoanRepayment()
        {
           // _io.StartOverDueCollection();
            _ic.StartRepaymentCollection();           
        }
        public void LoanFailedRepayment()
        {            
            _ic.StartFailedRepaymentCollection();
        }
        public void OtherSofriLoanFailedRepayment()
        {
            _io.StartOtherSofriLoanFailedRepaymentCollection();
        }
    }
}
