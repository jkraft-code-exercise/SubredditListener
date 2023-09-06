using Microsoft.Extensions.Options;
using SubredditListener.Configuration;
using SubredditListener.Services;

namespace SubredditListener
{
    public class RedditWorker : BackgroundService
    {
        private readonly ILogger<RedditWorker> _logger;

        private readonly IRedditClient _client;

        private readonly RedditClientConfiguration _config;

        private readonly IPostService _postService;

        public RedditWorker(ILogger<RedditWorker> logger, 
            IRedditClient client, 
            IOptions<RedditClientConfiguration> config, 
            IPostService postService)
        {
            _logger = logger;
            _client = client;
            _config = config.Value;
            _postService = postService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{name} running at: {time}", nameof(RedditWorker), DateTimeOffset.Now);

                await WatchSubreddits(stoppingToken);

                ReportStatistics();
            }
        }

        private void ReportStatistics()
        {         
            _postService.GetStatistics().ForEach(s => _logger.LogInformation("{stat}", s));
        }

        private async Task WatchSubreddits(CancellationToken stoppingToken)
        {
            var tasks = _config.Subreddits.Select(s => GetAllPosts(s, stoppingToken));
            await Task.WhenAll(tasks);
        }

        private async Task GetAllPosts(string subreddit, CancellationToken stoppingToken)
        {
            var requestedPostCount = 0;
            var actualPostCount = 0;
            string? after = null;

            _logger.LogInformation("Getting posts for subreddit '{subreddit}'", subreddit);

            while (requestedPostCount < _config.MaxPostsPerSubreddit)
            {
                var postListing = await _client.GetPostListing(subreddit, after, _config.MaxPostsPerRequest, stoppingToken);
                var posts = postListing?.Data?.Children;
                if (posts is not null)
                {
                    _postService.Add(posts);
                    actualPostCount += posts.Count;
                    _logger.LogDebug("Requested {requestedCount} and got {actualCount} posts for subreddit '{subreddit}'",
                        _config.MaxPostsPerRequest, posts.Count, subreddit);
                }

                requestedPostCount += _config.MaxPostsPerRequest;
                
                after = postListing?.Data?.After;

                if (after is null)
                {
                    _logger.LogInformation("Got {count} total posts for subreddit '{subreddit}'", actualPostCount, subreddit);
                    break;
                };
            }
        }
    }
}