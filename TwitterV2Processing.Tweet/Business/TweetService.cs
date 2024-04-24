using TwitterV2Processing.Tweet.Models;
using TwitterV2Processing.Tweet.Persistence;

namespace TwitterV2Processing.Tweet.Business
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public Task<TweetModel> CreateTweet(TweetModel tweet)
        {
            return _tweetRepository.CreateTweet(tweet);
        }

        public Task<List<TweetModel>> GetTweets()
        {
            return _tweetRepository.GetAllTweets();
        }

        public Task<List<TweetModel>> GetTweetsByUsername(string username)
        {
            return _tweetRepository.GetAllTweetsByUsername(username);
        }
    }
}
