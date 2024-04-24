namespace TwitterV2Processing.Tweet.DbSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TweetsCollectionName { get; set; } = null!;
    }
}
