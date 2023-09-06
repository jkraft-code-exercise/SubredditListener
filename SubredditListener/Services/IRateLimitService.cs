using RestSharp;

namespace SubredditListener.Services
{
    public interface IRateLimitService
    {
        Task CheckRateLimit(CancellationToken cancellationToken);

        void SetRateLimit(IReadOnlyCollection<HeaderParameter>? headers);        
    }
}