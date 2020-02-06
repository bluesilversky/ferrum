using Ferrum.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ferrum.Core.ServiceInterfaces
{
    public interface ICardAuthorisation 
    {
        Task<AuthoriseResponse> AuthoriseAsync(AuthoriseRequest request);
    }
}
