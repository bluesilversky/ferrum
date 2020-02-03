using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ferrum.Core.Structs
{
    public struct CardNumber
    {
        public readonly byte[] Digits;

        public int Length => Digits.Length;

        public bool IsValid => CardValidation.GetLuhnCheckValue(Digits) == 0;
        
        public CardNumber(string cardNumber)
        {
            Digits = CardValidation.CreateCardNumberByteArray(cardNumber);
        }

        internal CardNumber(byte[] digits)
        {
            Digits = digits;
        }        
    }

    
}
