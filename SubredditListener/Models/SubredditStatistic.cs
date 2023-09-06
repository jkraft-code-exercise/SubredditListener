namespace SubredditListener.Models
{
    public class SubredditStatistic
    {     
        public required string Subreddit { get; set; }

        public required int PostCount { get; set; }

        public required List<Post> TopPosts { get; set; }

        public required List<UserStatistic> TopUsers { get; set; }

        public override string ToString()
        {
            var topPostsHeader = TopPosts.FirstOrDefault()?.GetHeader();
            var topPostsSummary = $"""
                {topPostsHeader}
                {string.Join(Environment.NewLine, TopPosts)}
                """;

            var topUsersHeader = TopUsers.FirstOrDefault()?.GetHeader();
            var topUsersSumarry = $"""
                {topUsersHeader}
                {string.Join(Environment.NewLine, TopUsers)}
                """;

            return $"""             
                Statistics for subreddit "{Subreddit}" with {PostCount} posts
                
                Top Posts
                {topPostsSummary}

                Top Users
                {topUsersSumarry}
                """;
        }
    }
}
