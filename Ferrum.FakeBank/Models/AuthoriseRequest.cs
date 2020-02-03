using Ferrum.Core.Dtos;
using Ferrum.Core.Structs;

namespace Ferrum.FakeBank.Models
{
    public class AuthoriseRequest
    {
        private Card? _card;
        private CurrencyValue? _value;
        
        public CardDto Card { get; set; }
        public CurrencyValueDto Value { get; set; }
        public Card CardStruct => _card ??= new Card(Card.CardNumber, Card.ExpiryDate, Card.SecurityCode, Card.AccountHolderName);
        public CurrencyValue ValueStruct => _value ??= new CurrencyValue(Value.CurrencyCode, Value.Amount);        
    }
}
