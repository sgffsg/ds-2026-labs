using StackExchange.Redis;

namespace Valuator.Services;

public interface IRedisStorage
{
    Task SaveTextAsync(string id, string text);
    Task SaveRankAsync(string id, double rank);
    Task SaveSimilarityAsync(string id, double similarity);
    Task<string?> GetTextAsync(string id);
    Task<double?> GetRankAsync(string id);
    Task<double?> GetSimilarityAsync(string id);
    Task<bool> IsTextUniqueAsync(string text);
    Task AddToUniqueSetAsync(string text);
}

public class RedisStorage : IRedisStorage
{
    private readonly IDatabase _redis;
    private const string TEXT_KEY = "TEXT";
    private const string RANK_KEY = "RANK";
    private const string SIMILARITY_KEY = "SIMILARITY";
    private const string ALL_TEXT_KEY = "ALL_TEXT";

    public RedisStorage(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

   

    public Task SaveTextAsync(string id, string text)
    {
        return _redis.StringSetAsync(GetKey(TEXT_KEY, id), text);
    }

    public Task SaveRankAsync(string id, double rank)
    {
        return _redis.StringSetAsync(GetKey(RANK_KEY, id), rank);
    }

    public Task SaveSimilarityAsync(string id, double similarity)
    {
        return _redis.StringSetAsync(GetKey(SIMILARITY_KEY, id), similarity);
    }

    public async Task<string?> GetTextAsync(string id)
    {
        return await _redis.StringGetAsync(GetKey(TEXT_KEY, id));
    }

    public async Task<double?> GetRankAsync(string id)
    {
        var value = await _redis.StringGetAsync(GetKey(RANK_KEY, id));
        return value.HasValue ? (double)value : null;
    }

    public async Task<double?> GetSimilarityAsync(string id)
    {
        var value = await _redis.StringGetAsync(GetKey(SIMILARITY_KEY, id));
        return value.HasValue ? (double)value : null;
    }

    public Task AddToUniqueSetAsync(string text)
    {
        return _redis.SetAddAsync(ALL_TEXT_KEY, text);
    }

    public async Task<bool> IsTextUniqueAsync(string text)
    {
        var isUnique = await _redis.SetContainsAsync(ALL_TEXT_KEY, text);
        return !isUnique;
    }

    private string GetKey(string prefix, string id) => $"{prefix}-{id}";
}