using RestSharp;
using SubredditListener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener
{
    public interface IRedditClient
    {
        Task<Listing<Post>?> GetPostListing(string subreddit, string? after, int? limit, CancellationToken cancellationToken);
    }
}
