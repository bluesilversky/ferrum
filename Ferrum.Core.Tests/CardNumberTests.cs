using Ferrum.Core.Enums;
using Ferrum.Core.Structs;
using NUnit.Framework;
using System;

namespace Ferrum.Core.Tests
{
    public class CardNumberTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InvalidInstantiaionThrowsException()
        {
            static void invalidChars() => new CardNumber("1234x1234x1234x1234");
            static void tooLong() => new CardNumber("1234-1234-1234-1234-1234");
            static void tooShort() => new CardNumber("1234 1234 1234");

            Assert.Throws<ArgumentException>(invalidChars);
            Assert.Throws<ArgumentException>(tooLong);
            Assert.Throws<ArgumentException>(tooShort);
        }

        [Test]
        public void CardNumberLength()
        {
            var _16_digit = new CardNumber("1234 1234 1234 1234");
            var _15_digit = new CardNumber("123412341234123");
            var _19_digit = new CardNumber("1234 1234 1234 1234 123");

            Assert.AreEqual(16, _16_digit.Length);
            Assert.AreEqual(15, _15_digit.Length);
            Assert.AreEqual(19, _19_digit.Length);
        }

        [Test]
        public void AmexValidityTests()
        {
            var validAmex1 = new CardNumber("3434 783469 23497");
            var validAmex2 = new CardNumber("3773 500091 12107");
            var invalidAmex = new CardNumber("3434 783469 23498");
            var wrongStartsWith = new CardNumber("3534 783469 23498");
            var wrongLength = new CardNumber("3534 7834690 23498");

            Assert.AreEqual(CardNetwork.Amex, new Card() { CardNumber = validAmex1 }.CardNetwork);
            Assert.AreEqual(CardNetwork.Amex, new Card() { CardNumber = validAmex2 }.CardNetwork);
            Assert.AreEqual(CardNetwork.Amex, new Card() { CardNumber = invalidAmex }.CardNetwork);
            Assert.AreEqual(CardNetwork.Unknown, new Card() { CardNumber = wrongStartsWith }.CardNetwork);
            Assert.AreEqual(CardNetwork.Unknown, new Card() { CardNumber = wrongLength }.CardNetwork);

            Assert.IsTrue(validAmex1.IsValid);
            Assert.IsTrue(validAmex2.IsValid);
            Assert.IsFalse(invalidAmex.IsValid);
        }

        // Further validity test cases required for Mastercard, Visa & Discover
    }
}