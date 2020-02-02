using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ferrum.Core
{
    public struct CardNumber
    {
        public readonly byte[] Digits;

        public int Length => Digits.Length;

        public bool IsValid => CardNumberValidation.GetLuhnCheckValue(Digits) == 0;

        public CardNetwork CardNetwork => this.GetCardNetwork();

        public CardNumber(string cardNumber)
        {
            Digits = CardNumberValidation.CreateCardNumberByteArray(cardNumber);
        }

        internal CardNumber(byte[] digits)
        {
            Digits = digits;
        }        
    }

    internal static class CardNumberValidation
    {
        private static Regex _regex = new Regex("[0-9]{15,19}");

        internal static byte[] CreateCardNumberByteArray(string cardNumber)
        {
            var noSpaces = cardNumber.Replace(" ", "").Replace("-", "");
            if (noSpaces.Length < 15) throw new ArgumentException("Not long enough.", nameof(cardNumber));
            if (noSpaces.Length > 19) throw new ArgumentException("Too long.", nameof(cardNumber));

            var regexResult = _regex.Match(noSpaces);
            if (!regexResult.Success) throw new ArgumentException("Contains invalid characters. CardNumber can only contain digits 0-9 and space or dash seperators.", nameof(cardNumber));

            var result = noSpaces.Select(c => byte.Parse(c.ToString())).ToArray();
            return result;
        }

        internal static int GetLuhnCheckValue(byte[] digits)
        {
            //taken from https://www.rosettacode.org/wiki/Luhn_test_of_credit_card_numbers#C.23

            var result = digits.Select((d, i) => i % 2 == digits.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;

            return result;
        }
    }
}
