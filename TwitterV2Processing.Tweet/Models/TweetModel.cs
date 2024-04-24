using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TwitterV2Processing.Tweet.Models
{
    public class TweetModel
    {
        public TweetModel(string id, string content, string tweetername, int likes) {
            Id = id;
            Content = content;
            Tweetername = tweetername;
            Likes = likes;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        public string Tweetername { get; init; }
        public string Content { get; init; }
        public int Likes { get; init; }

    }
}
