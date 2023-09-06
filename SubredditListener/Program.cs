using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestSharp.Authenticators;
using SubredditListener;
using SubredditListener.Configuration;
using SubredditListener.Models;
using SubredditListener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((hostContext, services) =>
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOptions();
        services.Configure<RedditClientConfiguration>(hostContext.Configuration.GetSection("RedditClient"));
        services.AddSingleton<IAuthenticator, RedditAuthenticator>();
        services.AddSingleton<IRedditClient, RedditClient>();
        services.AddSingleton<IRateLimitService, PacedRateLimitService>();
        services.AddSingleton<IPostService, PostService>();
        services.AddHostedService<RedditWorker>();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("poststatistics", ([FromQuery] int? limit, IPostService postService) => postService.GetStatistics(limit ?? 10))
    .WithName("GetPostStatistics")
    .WithOpenApi(operation =>
    {
        var limitParameter = operation.Parameters[0];
        limitParameter.Description = "The number of Top Posts and Top Users to include in the Post Statistics";
        return operation;
    });

app.MapGet("posts/{id}", Results<Ok<Post>,
    NotFound> (string id, IPostService postService) => postService.Get(id) is Post post ? TypedResults.Ok(post) : TypedResults.NotFound())
    .WithName("GetPost")
    .WithOpenApi(operation =>
    {
        var idParameter = operation.Parameters[0];
        idParameter.Description = "The Post Id";
        return operation;
    });

app.Run();