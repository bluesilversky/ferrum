using Ferrum.Core.Enums.Serializable;
using System;

namespace Ferrum.Core.Models
{
    public class AuthoriseResponse
    {
        public Guid TransactionId { get; set; }

        public DateTime TimeStampUtc { get; set; }
        
        public AuthStatus AuthStatus { get; set; }
        
        public CardNetwork CardNetwork { get; set; }

        public string CurrencyCode { get; set; }
        
        public decimal Amount { get; set; }
    }
}
