using Ferrum.Core.Models;
using Ferrum.Core.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ferrum.Gateway.Integrations
{
    /*

    public static class PolicyHandlers
    {
        public static IAsyncPolicy<HttpResponseMessage> DefaultRetryPolicy()
        {
            var result = HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(0.5 * retryAttempt));
                        
        }
    }

    public static class CardAuthorisationExtensions
    {
        public static IServiceCollection AddCardAuthorisationClient(this IServiceCollection services, IConfiguration configuration)
        {
            var endpoint = configuration.GetSection("ConsumeServiceEndpoints")[nameof(CardAuthoriser)]; 

            services.AddHttpClient<ICardAuthorisation, CardAuthoriser>(nameof(CardAuthoriser), client =>
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddPolicyHandler(policy => policy;
        }
    }

    public class CardAuthoriser : ICardAuthorisation
    {


        public CardAuthoriser(IConfiguration configuration)
        {
            var endpoint =         
        }

        public async Task<AuthoriseResponse> AuthoriseAsync(AuthoriseRequest request)
        {
            await Task.Delay(100);
            return new AuthoriseResponse();
        }
    }

    */
}
