using Ferrum.Core.Enums.Serializable;
using Ferrum.Core.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferrum.Core.Domain
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime TimeStampUtc { get; set; }
        public ClientLogin ClientLogin { get; set; }
        public Client Client { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public CardNumber CardNumber { set; private get; }
        public string CardNumberEnding => 
            CardNumber.Digits.ToString().Substring(CardNumber.Length - 4);
        public AuthStatus AuthStatus { get; set; }
        public CardNetwork CardNetwork { get; set; }
        public int Attempts { get; set; }        
    }
}
