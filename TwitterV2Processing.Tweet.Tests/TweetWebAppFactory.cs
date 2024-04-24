using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.Persistence;

namespace TwitterV2Processing.Tweet.Tests
{
    internal class TweetWebAppFactory : WebApplicationFactory<Program>
    {
        private MongoDbRunner runner;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITweetRepository));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                runner = MongoDbRunner.Start();

                services.AddSingleton<IMongoClient>(new MongoClient(runner.ConnectionString));

                services.AddSingleton<ITweetRepository, TweetRepository>();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                runner.Dispose();
            }
        }
    }
}
