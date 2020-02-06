using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ferrum.Core.Domain;
using Ferrum.Core.Models;
using Ferrum.Core.Utils;
using Ferrum.Gateway.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ferrum.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardPaymentController : ControllerBase
    {
        private readonly ILogger<CardPaymentController> _logger;
        private readonly GatewayDbContext _dbContext;

        public CardPaymentController(ILogger<CardPaymentController> logger, GatewayDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
            
            defaultLogin.Client = null;
            
            return result; 
        }

        [HttpPost]
        [Route("authorise")]
        public async Task<Transaction> AuthoriseTransaction(AuthoriseRequest request)
        {
            await Task.Delay(100);
            return new Transaction();
        }
    }
}
