using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using SubredditListener.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SubredditListener
{
    public class RedditAuthenticator : AuthenticatorBase
    {
        private const string AccessTokenResource = "api/v1/access_token";

        private readonly ILogger<RedditAuthenticator> _logger;

        private readonly RedditClientConfiguration _config;       

        public RedditAuthenticator(ILogger<RedditAuthenticator> logger, IOptions<RedditClientConfiguration> config) : base("")
        {
            _config = config.Value;
            _logger = logger;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(_config.BaseAuthUrl)
            {
                Authenticator = new HttpBasicAuthenticator(_config.ClientId, _config.ClientSecret),
            };
            using var client = new RestClient(options);

            var request = new RestRequest(AccessTokenResource)
                .AddParameter("grant_type", "password")
                .AddParameter("username", _config.Username)
                .AddParameter("password", _config.Password);

            try
            {
                var restResponse = await client.ExecutePostAsync<TokenResponse>(request);
                if (restResponse.IsSuccessful)
                {
                    return $"{restResponse.Data!.TokenType} {restResponse.Data!.AccessToken}";
                }
                else
                {
                    _logger.LogError("Access token response was not successful.  Status code: {statusCode}, Error message: {errorMessage}",
                        restResponse.StatusCode, restResponse.ErrorMessage);
                    return null!;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get access token", ex);
                return null!;
            }     
        }
    }
    
    record TokenResponse
    {
        [JsonPropertyName("token_type")]
        public required string TokenType { get; init; }
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; init; }
    }
}
