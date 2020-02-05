﻿using Ferrum.Core.Enums.Serializable;
using Ferrum.Core.Structs;
using Ferrum.FakeBank.Validation.CardDate;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ferrum.FakeBank.Models
{
    public class AuthoriseRequest
    {
        [CreditCard]
        public string CardNumber { get; set; }
        
        [CardDate(CardDateType.ExpiryDate)]
        public string ExpiryDate { get; set; }
        
        [Range(0,9999, ErrorMessage = "Security Code must be between 000 and 9999.")]
        public int SecurityCode { get; set; }
        
        [Required]
        public string AccountHolder { get; set; }
        
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency Code must be 3 characters long.")]
        public string CurrencyCode { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        public AuthoriseResponse Respond(AuthStatus authStatus)
        {
            var response = new AuthoriseResponse()
            {
                Amount = Amount,
                CurrencyCode = CurrencyCode,
                AuthStatus = authStatus,
                TransactionId = Guid.NewGuid(),
                CardNetwork = new CardNumber(CardNumber).CardNetwork
            };

            return response;
        }
    }

    /*public class Card
    {
        
        public CardNetwork CardNetwork => CardNumber.GetCardNetwork();
        public MonthYear ExpiryDate { get; set; }
        public short SecurityCode { get; set; }
        public string AccountHolderName { get; set; }

        public Card(string cardNumber, string expiry, int securityCode, string accountHolder)
        {
            CardNumber = new CardNumber(cardNumber);
            ExpiryDate = new MonthYear(expiry);
            SecurityCode = CardValidation.ParseSecurityCode(securityCode);
            AccountHolderName = accountHolder;
        }
    }*/
}
