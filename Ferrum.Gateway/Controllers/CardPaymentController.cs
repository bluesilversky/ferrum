using Ferrum.Core.Domain;
using Ferrum.Core.Models;
using Ferrum.Core.ServiceInterfaces;
using Ferrum.Core.Structs;
using Ferrum.Gateway.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Ferrum.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardPaymentController : ControllerBase
    {
        private readonly ILogger<CardPaymentController> _logger;
        private readonly GatewayDbContext _dbContext;
        private readonly ICardAuthorisation _cardAuthoriser;

        public CardPaymentController(ILogger<CardPaymentController> logger, 
            GatewayDbContext dbContext, 
            ICardAuthorisation cardAuthoriser)
        {
            _logger = logger;
            _dbContext = dbContext;
            _cardAuthoriser = cardAuthoriser;
        }
        
        [HttpGet]
        [Route("login")]
        public async Task<Client> GetLogin()
        {
            var result = await _dbContext.Clients
                .AsNoTracking()
                .Include(c => c.ClientLogins)
                .FirstOrDefaultAsync(c => c.Name == GatewayContextSeeder.DefaultClientName);
                
            var defaultLogin = result.ClientLogins.FirstOrDefault();
            
            return result; 
        }

        [HttpPost]
        [Route("authorise")]
        public async Task<Transaction> AuthoriseTransaction(AuthoriseRequest request)
        {
            var response = _cardAuthoriser.AuthoriseAsync(request);
            var clientLogin = _dbContext.ClientLogins.FirstOrDefaultAsync(cl => cl.LoginName == GatewayContextSeeder.DefaultLoginName);

            Task.WaitAll(response, clientLogin);

            var transaction = new Transaction
            {
                Amount = response.Result.Amount,
                AuthStatus = response.Result.AuthStatus,
                CardNetwork = response.Result.CardNetwork,
                CardNumber = new CardNumber(request.CardNumber),
                ClientId = clientLogin.Result.ClientId,
                ClientLoginId = clientLogin.Result.Id,
                CurrencyCode = request.CurrencyCode,
                TimeStampUtc = response.Result.TimeStampUtc
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }
    }
}
