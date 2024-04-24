namespace TwitterV2Processing.Tweet.DbSettings
{
    public interface IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public string TweetsCollectionName { get; set; }
    }
}
