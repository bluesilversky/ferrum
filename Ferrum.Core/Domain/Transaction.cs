using Ferrum.Core.Enums.Serializable;
using Ferrum.Core.Structs;
using System;

namespace Ferrum.Core.Domain
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime TimeStampUtc { get; set; }
        public int ClientId { get; set; }
        public int ClientLoginId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public CardNumber CardNumber { set; private get; }
        public string CardNumberEnding { get; set; }        
        public AuthStatus AuthStatus { get; set; }
        public CardNetwork CardNetwork { get; set; }
        public int Attempts { get; set; }        
    }
}
