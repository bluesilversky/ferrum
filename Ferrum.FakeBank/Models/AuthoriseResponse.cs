using Ferrum.Core.Enums;
using System;

namespace Ferrum.FakeBank.Models
{
    public class AuthoriseResponse
    {
        public Guid TransactionId { get; set; }
        
        public AuthStatus AuthStatus { get; set; }

        public AuthoriseResponse(AuthStatus status)
        {
            TransactionId = Guid.NewGuid();
            AuthStatus = status;
        }
    }
}
