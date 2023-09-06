using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SubredditListener.Models
{
    public record Listing<T> : Thing<ListingData<T>> { }

    public record ListingData<T>
    {
        [JsonPropertyName("after")]
        public required string After { get; set; }

        [JsonPropertyName("dist")]
        public required int Dist { get; set; }

        [JsonPropertyName("children")]
        public required List<T> Children { get; set; }

        [JsonPropertyName("before")]
        public required string Before { get; set; }
    }
}