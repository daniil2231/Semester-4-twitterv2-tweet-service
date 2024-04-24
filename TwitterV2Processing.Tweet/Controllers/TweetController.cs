using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.Business;
using TwitterV2Processing.Tweet.Models;
using TwitterV2Processing.Tweet.Persistence;

namespace TwitterV2Processing.Tweet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ILogger<TweetController> _logger;
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService, ILogger<TweetController> logger)
        {
            _logger = logger;
            _tweetService = tweetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tweets = await _tweetService.GetTweets();

            return Ok(tweets);
        }

        [HttpGet("GetAllByUsername")]
        public async Task<IActionResult> GetAllByUsername(string username)
        {
            var tweets = await _tweetService.GetTweetsByUsername(username);

            return Ok(tweets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTweet([FromBody]TweetModel tweet)
        {
            var newTweet = await _tweetService.CreateTweet(tweet);

            return Ok(newTweet);
        }
    }
}
