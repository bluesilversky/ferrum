using Ferrum.Core.Domain;
using Ferrum.Core.Models;
using Ferrum.Core.ServiceInterfaces;
using Ferrum.Core.Structs;
using Ferrum.Gateway.Authorisation;
using Ferrum.Gateway.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ferrum.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(AuthoriseClient))]
    public class CardPaymentController : ControllerBase
    {
        private readonly GatewayDbContext _dbContext;
        private readonly ICardAuthorisation _cardAuthoriser;

        public CardPaymentController(
            GatewayDbContext dbContext, 
            ICardAuthorisation cardAuthoriser)
        {
            _dbContext = dbContext;
            _cardAuthoriser = cardAuthoriser;
        }
        
        [HttpPost]
        [Route("authorise")]
        public async Task<AuthoriseResponse> AuthoriseTransaction(AuthoriseRequest request)
        {
            var user = RouteData.GetUser();
            var cardNumber = new CardNumber(request.CardNumber);
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            var response = await _cardAuthoriser.AuthoriseAsync(request);
            stopwatch.Stop();
            response.ProcessingTimeMs = Convert.ToInt32(stopwatch.ElapsedMilliseconds);

            var transaction = Transaction.Create(response, cardNumber, user);
            
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return response;
        }
    }
}
