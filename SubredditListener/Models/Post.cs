using System;
using System.Text.Json.Serialization;

namespace SubredditListener.Models
{
    public record Post : Thing<PostData> {
        public string GetHeader()
        {
            return $"|{nameof(Data.Ups),-10}|{nameof(Data.Id),-10}|{nameof(Data.Author),-30}|{nameof(Data.Permalink),-90}|";
        }        

        public override string ToString()
        {
            return $"|{Data.Ups,10}|{Data.Id,10}|{Data.Author,30}|{Data.Permalink,90}|";
        }
    }

    public record PostData
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("permalink")]
        public required string Permalink { get; set; }

        [JsonPropertyName("subreddit")]
        public required string Subreddit { get; set; }

        [JsonPropertyName("author")]
        public required string Author { get; set; }

        [JsonPropertyName("author_fullname")]
        public string? AuthorFullname { get; set; }

        [JsonPropertyName("ups")]
        public required int Ups { get; set; }

        [JsonPropertyName("downs")]
        public required int Downs { get; set; }

        [JsonPropertyName("upvote_ratio")]
        public required double UpvoteRatio { get; set; }        
    }
}
