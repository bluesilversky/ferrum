using Ferrum.Core.Models;
using Ferrum.Core.Extensions;
using Ferrum.Core.ServiceInterfaces;
using Ferrum.Core.Enums.Serializable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Http;
using System.Linq;

namespace Ferrum.Gateway.Integrations
{
    public static class PolicyHandlers
    {
        public static int[] RetryIntervals = { 50, 100, 250, 500, 1000, 1500 };

        public static TimeSpan PredefinedCapped(int retryAttempt)
        {
            var ms = retryAttempt > RetryIntervals.Length ?
                RetryIntervals.Last() : RetryIntervals[retryAttempt - 1];

            return TimeSpan.FromMilliseconds(ms);
        }

        public static IAsyncPolicy<HttpResponseMessage> LogRetryPolicy()
        {
            //var newRetryTrackingService = serviceProvider.GetService<IHttpRetryTrackingService>();

            var result = HttpPolicyExtensions.HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
            .WaitAndRetryAsync(retryCount: 10, sleepDurationProvider: PredefinedCapped,
                onRetry: (response, delay, retryCount, context) =>
                     {
                         response.Result.Headers.Add("RetryAttempts", retryCount.ToString());
                         context.
                     }
                );
            return result;
        }
    }

    public interface IHttpRetryTrackingService { }
    public class RetryTrackingService { }

    public class Counter
    {
            public Guid TransactionId { get; set; }
            public int Count { get; set; }
    }

    public static class CardAuthorisationExtensions
    {
        public static IServiceCollection AddCardAuthorisationClient(this IServiceCollection services)
        {
            services.AddHttpClient<ICardAuthorisation, CardAuthoriser>()
                .AddPolicyHandler(PolicyHandlers.LogRetryPolicy());

            return services;
        }
    }

    public class CardAuthoriser : ICardAuthorisation
    {
        private readonly HttpClient _client;
        public CardAuthoriser(HttpClient httpClient, IConfiguration configuration)
        {
            _client = httpClient;
            
            var endpoint = configuration.GetSection("ConsumeServiceEndpoints")[nameof(CardAuthoriser)];

            _client.BaseAddress = new Uri(endpoint);
        }

        public async Task<AuthoriseResponse> AuthoriseAsync(AuthoriseRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var httpResponse = await _client.PostAsync("api/authorise", content); //maybe just /authorise now.
            httpResponse.Headers.TryGetValues("RetryAttempts", out var headerValues);
            var retryCount = int.Parse(headerValues.First());

            if (!httpResponse.IsSuccessStatusCode)
            { 
                var errorResponse = request.Respond(AuthStatus.Error);
                
                errorResponse.RetryAttempts = retryCount;
                
                return errorResponse;
            }

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.AddEnumSerializers();

            var result = JsonSerializer.Deserialize<AuthoriseResponse>(stringResponse, options);
            result.RetryAttempts = retryCount;

            return result;
        }
    }
}
