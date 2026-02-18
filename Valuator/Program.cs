using StackExchange.Redis;
using Valuator.Builders;
using Valuator.Services;

namespace Valuator;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "cl-redis:6379";
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);
        builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

        builder.Services.AddScoped<IRedisStorage, RedisStorage>();
        builder.Services.AddScoped<ITextRankCalculator, TextRankCalculator>();
        builder.Services.AddScoped<ITextEvaluationService, TextEvaluationService>();

        builder.Services.AddTransient<ITextEvaluationResultBuilder, TextEvaluationResultBuilder>();

        builder.Services.AddRazorPages();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        app.Run();
    }
}