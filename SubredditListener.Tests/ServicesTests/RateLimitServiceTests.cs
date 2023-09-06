using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using RestSharp;
using SubredditListener.Configuration;
using SubredditListener.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener.Tests.ServicesTests
{
    public class RateLimitServiceTests
    {
        private static RateLimitService CreateDefaultRateLimitService()
        {
            var options = Options.Create(new RedditClientConfiguration
            {
                BaseApiUrl = "TestBaseApiUrl",
                BaseAuthUrl = "TestBaseAuthUrl",
                ClientId = "TestClientId",
                ClientSecret = "TestClientSecret",
                MaxPostsPerRequest = 100,
                MaxPostsPerSubreddit = 1000,
                Password = "TestPassword",
                Subreddits = new string[2] { "TestSubreddit1", "TestSubreddit2" },
                UserAgent = "TestUserAgent",
                Username = "TestUsername"
            });

            return new RateLimitService(new NullLogger<RedditWorker>(), options);
        }

        [Theory]
        [MemberData(nameof(Headers), (double)10, 20)]
        public void SetRateLimit_WithHeaders_ShouldUpdateLimits(List<HeaderParameter> headers)
        {
            var rateLimitService = CreateDefaultRateLimitService();
                        
            rateLimitService.SetRateLimit(headers);

            Assert.Equal(10, rateLimitService.RateLimitRemaining);
            Assert.Equal(20, rateLimitService.RateLimitReset);
        }

        [Fact]        
        public void SetRateLimit_WithoutHeaders_ShouldThrowArgumentNullException()
        {
            var rateLimitService = CreateDefaultRateLimitService();

            void action() => rateLimitService.SetRateLimit(null);

            Assert.Throws<ArgumentNullException>(action);
        }

        public static IEnumerable<object[]> Headers(double rateLimitRemaining, int rateLimitReset)
        {
            return new List<object[]>
            { 
                new object[] {
                    new List<HeaderParameter>
                    {
                        new HeaderParameter("x-ratelimit-remaining", rateLimitRemaining.ToString()),
                        new HeaderParameter("x-ratelimit-reset", rateLimitReset.ToString())
                    }                    
                }
            };
        }
    }
}
