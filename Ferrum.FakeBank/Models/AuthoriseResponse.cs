using Ferrum.Core.Enums;
using System;

namespace Ferrum.FakeBank.Models
{
    public class AuthoriseResponse
    {
        public Guid TransactionId { get; set; }
        public AuthStatus Status { get; set; }

        public AuthoriseResponse(AuthStatus status)
        {
            TransactionId = Guid.NewGuid();
            Status = status;
        }
    }
}
