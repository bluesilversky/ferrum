using Ferrum.Core;

namespace Ferrum.FakeBank.Models
{
    public class AuthoriseRequest
    {
        private CardNumber? _cardNumber;
        public string CardNumber { get; set; }
        public int SecurityCode { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public string AccountHolder { get; set; }
        public CardNumber CardStruct => _cardNumber ??= new CardNumber(CardNumber);
    }
}
