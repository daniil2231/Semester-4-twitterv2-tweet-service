using Confluent.Kafka;
using MongoDB.Driver;
using Newtonsoft.Json;
using TwitterV2Processing.Tweet.Persistence;

namespace TwitterV2Processing.Tweet.Business
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig consumerConfig;
        private readonly ITweetRepository _tweetRepository;

        public ConsumerService(ILogger<ConsumerService> logger, ITweetRepository tweetRepository)
        {
            _logger = logger;
            _tweetRepository = tweetRepository;

            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "kafka:9092",
                GroupId = "tweet_service",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("user_deletion");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(async () => ProcessKafkaMessage(stoppingToken), stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            _consumer.Close();
        }

        public async Task ProcessKafkaMessage(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                var messageJson = consumeResult.Message.Value;
                var message = JsonConvert.DeserializeObject<dynamic>(messageJson);

                // Extract username from the message
                var username = (string)message.Username;

                await DeleteTweetsByUser(username);

                _logger.LogInformation($"Received inventory update: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }

        public async Task<DeleteResult> DeleteTweetsByUser(string username)
        {
            return await _tweetRepository.DeleteTweetsByUser(username);
        }
    }
}
