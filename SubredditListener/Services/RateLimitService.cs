using Microsoft.Extensions.Options;
using RestSharp;
using SubredditListener.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener.Services
{
    public class RateLimitService : IRateLimitService
    {
        protected const int DefaultRateLimitReset = 600;

        public double? RateLimitRemaining { get; private set; }

        public int? RateLimitReset { get; private set; }

        public double? MinRateLimitRemaining { get; private set; }

        private readonly ILogger<RedditWorker> _logger;

        public RateLimitService(ILogger<RedditWorker> logger, IOptions<RedditClientConfiguration> config)
        {
            _logger = logger;
            MinRateLimitRemaining = config.Value.Subreddits.Length;
        }

        public void SetRateLimit(IReadOnlyCollection<HeaderParameter>? headers)
        {
            if (headers is null) throw new ArgumentNullException(nameof(headers));

            RateLimitRemaining = TryGetHeaderValue<double>(headers, "x-ratelimit-remaining", double.TryParse);

            RateLimitReset = TryGetHeaderValue<int>(headers, "x-ratelimit-reset", int.TryParse);

            _logger.LogDebug("Updated rate limit remaining: {rateLimitRemaining}, rate limit reset: {rateLimitReset}", RateLimitRemaining, RateLimitReset);
        }

        public virtual async Task CheckRateLimit(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Checking rate limit remaining: {rateLimitRemaining}, minimum rate limit remaining: {minRateLimitRemaining}, rate limit reset: {rateLimitReset}", RateLimitRemaining, MinRateLimitRemaining, RateLimitReset);

            if (RateLimitRemaining < MinRateLimitRemaining && RateLimitReset is not null)
            {
                var rateLimitReset = RateLimitReset ?? DefaultRateLimitReset;
                _logger.LogInformation("Rate limit reached.  Delaying for {rateLimitReset} seconds", rateLimitReset);
                await Task.Delay(TimeSpan.FromSeconds(rateLimitReset), cancellationToken);
            }
        }

        private delegate bool TryParse<T>(string str, out T value);

        private static T? TryGetHeaderValue<T>(IReadOnlyCollection<HeaderParameter>? headers, string headerName, TryParse<T> tryParseFunc)
        {
            var header = headers?.FirstOrDefault(h => headerName.Equals(h.Name, StringComparison.OrdinalIgnoreCase));

            if (header?.Value is not null && tryParseFunc((string)header.Value, out T value))
            {
                return value;
            }

            return default;
        }
    }
}
