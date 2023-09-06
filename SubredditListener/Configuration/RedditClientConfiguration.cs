using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener.Configuration
{
    public class RedditClientConfiguration
    {
        public required string BaseAuthUrl { get; set; }

        public required string BaseApiUrl { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string ClientId { get; set; }

        public required string ClientSecret { get; set; }

        public required string UserAgent { get; set; }

        public required string[] Subreddits { get; set; }

        public required int MaxPostsPerRequest { get; set; }

        public required int MaxPostsPerSubreddit { get; set; }
    }
}
