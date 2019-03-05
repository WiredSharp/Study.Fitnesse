using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.Fit.MyProject
{
    public class ShouldIBuyMilk
    {
        public int CashInWallet {get;set;}

        public string CreditCard { get; set; }

        public int PintsOfMilkRemaining { get; set; }

        public string GoToStore()
        {
            if (CashInWallet > 0 || CreditCard.Equals("yes"))
                return "yes";
            return "no";
        }
    }
}
