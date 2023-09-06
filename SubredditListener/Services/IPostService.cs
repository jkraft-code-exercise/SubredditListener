using SubredditListener.Models;

namespace SubredditListener.Services
{
    public interface IPostService
    {
        void Add(List<Post> posts);

        Post? Get(string Id);

        List<Post> GetAll();

        List<SubredditStatistic> GetStatistics(int limit = 10);
    }
}