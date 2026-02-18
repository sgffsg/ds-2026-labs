namespace Valuator.Services.Interfaces
{
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
}
