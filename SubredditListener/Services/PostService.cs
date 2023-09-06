using SubredditListener.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubredditListener.Services
{
    public class PostService : IPostService
    {
        private readonly ConcurrentDictionary<string, Post> _repository = new();

        public void Add(List<Post> posts)
        {
            foreach (var post in posts)
            {
                _repository.AddOrUpdate(post.Data.Id, post, (key, oldValue) => post);
            }
        }

        public List<Post> GetAll()
        {
            return new List<Post>(_repository.Values);
        }

        public Post? Get(string id)
        {
            return _repository.Values.FirstOrDefault(p => p.Data.Id == id);
        }

        public List<SubredditStatistic> GetStatistics(int limit = 10)
        {
            return
                GetAll()
                .GroupBy(p => p.Data.Subreddit)
                .Select(g => new SubredditStatistic
                {
                    PostCount = g.Count(),
                    Subreddit = g.Key,
                    TopPosts = g.OrderByDescending(p => p.Data.Ups).Take(limit).ToList(),
                    TopUsers = g.GroupBy(p => p.Data.AuthorFullname)
                    .Select(g2 => new UserStatistic
                    {
                        AuthorFullName = g2.Key,
                        Author = g2.First().Data.Author,
                        PostCount = g2.Count()
                    })
                    .OrderByDescending(p => p.PostCount)
                    .Take(limit)
                    .ToList()
                })
                .ToList();
        }
    }
}
