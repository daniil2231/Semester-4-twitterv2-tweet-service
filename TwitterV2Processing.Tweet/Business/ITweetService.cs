using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.Models;

namespace TwitterV2Processing.Tweet.Business
{
    public interface ITweetService
    {
        public Task<List<TweetModel>> GetTweets();

        public Task<List<TweetModel>> GetTweetsByUsername(string username);

        public Task<TweetModel> CreateTweet(TweetModel tweet);

       // public Task<DeleteResult> DeleteTweetsByUser(string username);
    }
}
