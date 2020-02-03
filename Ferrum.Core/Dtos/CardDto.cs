using System;
using System.Collections.Generic;
using System.Text;

namespace Ferrum.Core.Dtos
{
    public class CardDto
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int SecurityCode { get; set; }
        public string AccountHolderName { get; set; }
    }
}
