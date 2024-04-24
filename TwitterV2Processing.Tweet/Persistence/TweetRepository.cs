using MongoDB.Bson;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.DbSettings;
using TwitterV2Processing.Tweet.Models;

namespace TwitterV2Processing.Tweet.Persistence
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IMongoCollection<TweetModel> _tweets;

        public TweetRepository(IDatabaseSettings settings, IMongoClient client) {
            var db = client.GetDatabase(settings.DatabaseName);
            _tweets = db.GetCollection<TweetModel>(settings.TweetsCollectionName);
        }

        public async Task<TweetModel> CreateTweet(TweetModel tweet)
        {
            await _tweets.InsertOneAsync(tweet);
            return tweet;
        }

        public async Task DeleteTweet(ObjectId id)
        {
            await _tweets.DeleteOneAsync(tweet => tweet.Id.Equals(id));
        }

        public async Task<List<TweetModel>> GetAllTweets()
        {
            return await _tweets.Find(tweet => true).ToListAsync();
        }

        public async Task<List<TweetModel>> GetAllTweetsByUsername(string username)
        {
            return await _tweets.Find<TweetModel>(tweet => tweet.Tweetername == username).ToListAsync();
        }
    }
}
