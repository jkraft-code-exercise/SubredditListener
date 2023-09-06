namespace SubredditListener.Models
{
    public class UserStatistic
    {
        public string? AuthorFullName { get; set; }

        public required string Author { get; set; }

        public required int PostCount { get; set; }

        public string GetHeader()
        {
            return $"|{nameof(PostCount),-10}|{nameof(AuthorFullName),-20}|{nameof(Author),-30}|";
        }

        public override string ToString()
        {
            return $"|{PostCount,10}|{AuthorFullName,20}|{Author,30}|";
        }
    }
}
