using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitterV2Processing.Tweet.Models;

namespace TwitterV2Processing.Tweet.Tests
{
    internal class Integration
    {
        private WebApplicationFactory<Program> _application;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _application = new TweetWebAppFactory();
            _client = _application.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _application?.Dispose();
            _client?.Dispose();
        }

        [Test]
        public async Task Post_ShouldReturn_CreatedTweet()
        {
            // Arrange
            TweetModel newTweet = new TweetModel("", "test tweet", "test1", 0);
            var createRes = await _client.PostAsJsonAsync("/api/Tweet", newTweet);
            createRes.EnsureSuccessStatusCode();

            // Act
            var response = await _client.GetAsync("/api/Tweet");

            // Assert
            var tweets = await response.Content.ReadFromJsonAsync<List<TweetModel>>();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(tweets, Is.Not.Null);
                Assert.That(tweets.Count, Is.GreaterThan(0));
                Assert.That(tweets.Any(t => t.Content == newTweet.Content));
            });
        }

        [Test]
        public async Task GetAllTweets_ShouldReturn_Tweets()
        {
            // Arrange
            var newTweets = new List<TweetModel> {
                new TweetModel("", "test tweet", "test1", 0),
                new TweetModel("", "test tweet 2", "test1", 0)
            };

            foreach (TweetModel tweet in newTweets)
            {
                var postRes = await _client.PostAsJsonAsync("/api/Tweet", tweet);
                postRes.EnsureSuccessStatusCode();
            }

            // Act
            var res = await _client.GetAsync("/api/Tweet");

            // Assert
            var tweets = await res.Content.ReadFromJsonAsync<List<TweetModel>>();
            // If any of the assertions fail, the test will still continue running and will report all the failed assertions at the end.
            // This allows you to get a complete picture of what went wrong in the test, rather than stopping at the first failure.
            // Useful when testing multiple components such as in an integration test.
            Assert.Multiple(() =>
            {
                Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(res.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));
                Assert.That(tweets, Is.Not.Null);
                Assert.That(tweets.Count, Is.GreaterThan(0));
            });
        }
    }
}
