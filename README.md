# twitterv2-tweet-service
- The tweet service is responsible for handling CRUD operations for tweets.
- Has its own database.
- Receives API calls through an API gateway.
- Uses Kafka to consume messages in situations when a change to an existing user account occurs in order to stay up to date.

![Architecture Diagram](https://github.com/daniil2231/twitterv2-tweet-service/blob/main/c4_tweet.png)
