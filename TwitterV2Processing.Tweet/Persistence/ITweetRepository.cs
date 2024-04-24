using MongoDB.Bson;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.Models;

namespace TwitterV2Processing.Tweet.Persistence
{
    public interface ITweetRepository
    {
        Task<List<TweetModel>> GetAllTweets();
        Task<List<TweetModel>> GetAllTweetsByUsername(string username);
        Task<TweetModel> CreateTweet(TweetModel tweet);
        Task DeleteTweet(ObjectId id);
    }
}
