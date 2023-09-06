using SubredditListener.Services;

namespace SubredditListener.Tests.ServicesTests
{
    public class PostServiceTests
    {
        private static PostService CreateDefaultPostService()
        {
            return new PostService();
        }

        [Theory]
        [MemberData(nameof(Posts))]
        public void Add_Posts_AddsAll(List<Models.Post> posts)
        {
            var postService = CreateDefaultPostService();
            
            postService.Add(posts);

            Assert.Equal(posts.Count, postService.GetAll().Count);
        }

        [Theory]
        [MemberData(nameof(Posts))]
        public void Add_SamePosts_Replaces(List<Models.Post> posts)
        {
            var postService = CreateDefaultPostService();

            postService.Add(posts);
            postService.Add(posts);

            Assert.Equal(posts.Count, postService.GetAll().Count);
        }

        [Theory]
        [MemberData(nameof(Posts))]
        public void Get_PostById_GetsPost(List<Models.Post> posts)
        {
            var postService = CreateDefaultPostService();

            var testId = posts.First().Data.Id;
            postService.Add(posts);
            var post = postService.Get(testId);
                        
            Assert.NotNull(post);            
        }

        [Fact]
        public void GetAll_WithNoPosts_IsEmpty()
        {
            var postService = CreateDefaultPostService();

            var posts = postService.GetAll();

            Assert.Empty(posts);
        }

        [Fact]
        public void GetStatistics_WithNoPosts_IsEmpty()
        {
            var postService = CreateDefaultPostService();

            var statistics = postService.GetStatistics();

            Assert.Empty(statistics);
        }

        [Theory]
        [MemberData(nameof(Posts))]
        public void GetStatistics_WithPosts_IsNotEmpty(List<Models.Post> posts)
        {
            var postService = CreateDefaultPostService();
                        
            postService.Add(posts);
            var statistics = postService.GetStatistics();

            Assert.NotEmpty(statistics);
        }

        public static IEnumerable<object[]> Posts => new List<object[]>
        {
            new object[] {
                new List<Models.Post> {
                    new Models.Post
                    {                        
                        Data = new Models.PostData
                        {
                            Author = "TestAuthor1",
                            AuthorFullname = "TestAuthorFullName1",
                            Downs = 5,
                            Id = "TestId1",
                            Name = "TestName1",
                            Permalink = "TestPermalink1",
                            Subreddit = "TestSubreddit1",
                            Title = "TestTitle1",
                            Ups = 10,
                            UpvoteRatio = 0.5
                        },
                        Kind = "TestKind1"
                    },
                    new Models.Post
                    {
                        Data = new Models.PostData
                        {
                            Author = "TestAuthor2",
                            AuthorFullname = "TestAuthorFullName2",
                            Downs = 10,
                            Id = "TestId2",
                            Name = "TestName2",
                            Permalink = "TestPermalink2",
                            Subreddit = "TestSubreddit2",
                            Title = "TestTitle2",
                            Ups = 100,
                            UpvoteRatio = 0.9
                        },
                        Kind = "TestKind2"
                    },
                }
            }
        };
    }
}