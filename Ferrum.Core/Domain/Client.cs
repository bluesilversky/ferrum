using System;
using System.Collections.Generic;
using System.Text;

namespace Ferrum.Core.Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public ICollection<ClientLogin> ClientLogins { get; set; } = new List<ClientLogin>();
    }
}
