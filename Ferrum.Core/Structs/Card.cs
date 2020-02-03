﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Ferrum.Core.Enums;

namespace Ferrum.Core.Structs
{
    public struct Card
    {
        public CardNumber CardNumber { get; set; }
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
    }    
}