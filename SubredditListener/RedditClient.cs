using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using SubredditListener.Configuration;
using SubredditListener.Models;
using SubredditListener.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener
{
    internal class RedditClient : IRedditClient, IDisposable
    {
        private readonly ILogger<RedditClient> _logger;

        private readonly RedditClientConfiguration _config;

        private readonly RestClient _client;

        private readonly IRateLimitService _rateLimitService;

        public RedditClient(ILogger<RedditClient> logger, IOptions<RedditClientConfiguration> config, IAuthenticator authenticator, IRateLimitService rateLimitService)
        {
            _logger = logger;
            _config = config.Value;
            _rateLimitService = rateLimitService;

            var options = new RestClientOptions(_config.BaseApiUrl)
            {
                Authenticator = authenticator,
                UserAgent = _config.UserAgent
            };

            _client = new RestClient(options);

            _logger.LogInformation("{name} created", nameof(RedditClient));
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Listing<Post>?> GetPostListing(string subreddit, string? after, int? limit, CancellationToken cancellationToken)
        {
            if (limit > 100) throw new ArgumentOutOfRangeException(nameof(limit));

            await _rateLimitService.CheckRateLimit(cancellationToken);
                        
            // This could be extended to support filters other than "new"
            var request = new RestRequest($"r/{subreddit}/new")
                .AddParameter("after", after)
                .AddParameter("limit", limit?.ToString());

            try
            {
                var restResponse = await _client.ExecuteGetAsync<Listing<Post>>(request, cancellationToken);
                if (restResponse.IsSuccessful)
                {
                    _rateLimitService.SetRateLimit(restResponse.Headers);
                    return restResponse.Data;
                } 
                else
                {
                    if (restResponse.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        _rateLimitService.SetRateLimit(restResponse.Headers);
                    }

                    _logger.LogError("Post listing response was not successful.  Status code: {statusCode}, Error message: {errorMessage}",
                        restResponse.StatusCode, restResponse.ErrorMessage);
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get post listing", ex);
                return null;
            }            
        }
    }
}
