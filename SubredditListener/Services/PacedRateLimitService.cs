using Microsoft.Extensions.Options;
using SubredditListener.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener.Services
{
    public class PacedRateLimitService : RateLimitService
    {
        private readonly ILogger<RedditWorker> _logger;

        private readonly RedditClientConfiguration _config;

        public PacedRateLimitService(ILogger<RedditWorker> logger, IOptions<RedditClientConfiguration> config) : base(logger, config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public override async Task CheckRateLimit(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Checking rate limit remaining: {rateLimitRemaining}, minimum rate limit remaining: {minRateLimitRemaining}, rate limit reset: {rateLimitReset}", 
                RateLimitRemaining, MinRateLimitRemaining, RateLimitReset);

            var delayMultiple = _config.Subreddits.Length > 1 ? _config.Subreddits.Length - 1 : 1;
            var delay = RateLimitRemaining is not null && RateLimitReset is not null
                ? RateLimitReset.Value / RateLimitRemaining.Value * delayMultiple
                : 1;

            _logger.LogInformation("Delaying for {delay:0.####} seconds to stay within rate limit of {rateLimitRemaining} which resets in {rateLimitReset} seconds",
                delay, RateLimitRemaining, RateLimitReset);
            await Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken);                        

            if (RateLimitRemaining < MinRateLimitRemaining && RateLimitReset is not null)
            {
                var rateLimitReset = RateLimitReset ?? DefaultRateLimitReset;
                _logger.LogInformation("Rate limit reached.  Delaying for {rateLimitReset} seconds", rateLimitReset);
                await Task.Delay(TimeSpan.FromSeconds(rateLimitReset), cancellationToken);
            }
        }
    }
}
