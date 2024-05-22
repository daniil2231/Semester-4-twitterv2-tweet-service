using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TwitterV2Processing.Tweet.Business;
using TwitterV2Processing.Tweet.DbSettings;
using TwitterV2Processing.Tweet.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("TweetDatabase"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<ITweetRepository, TweetRepository>();
builder.Services.AddSingleton<ITweetService, TweetService>();
builder.Services.AddHostedService<ConsumerService>();

builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetValue<string>("TweetDatabase:ConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
