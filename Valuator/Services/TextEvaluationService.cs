using Valuator.Builders;
using Valuator.Models;

namespace Valuator.Services;

public interface ITextEvaluationService
{
    Task<string> EvaluateAsync(string text);
    Task<TextEvaluationResult?> GetResultAsync(string id);
}

public class TextEvaluationService : ITextEvaluationService
{
    #region init
    private readonly ITextRankCalculator _textRankCalculator;
    private readonly ITextEvaluationResultBuilder _textEvaluationResultBuilder;
    private readonly IRedisStorage _redisStorage;

    public TextEvaluationService(
        ITextRankCalculator calculator,
        ITextEvaluationResultBuilder builder,
        IRedisStorage storage
    )
    {
        _textRankCalculator = calculator;
        _textEvaluationResultBuilder = builder;
        _redisStorage = storage;
    }

    #endregion

    public async Task<string> EvaluateAsync(string text)
    {
        string id = Guid.NewGuid().ToString();
        double rank = _textRankCalculator.Calculate(text);
        bool isUnique = await _redisStorage.IsTextUniqueAsync(text);
        double similarity = isUnique ? 0.0 : 1.0;

        await _redisStorage.SaveTextAsync(id, text);
        await _redisStorage.SaveRankAsync(id, rank);
        await _redisStorage.SaveSimilarityAsync(id, similarity);

        if (isUnique)
        {
            await _redisStorage.AddToUniqueSetAsync(text);
        }

        return id;
    }

    public async Task<TextEvaluationResult?> GetResultAsync(string id)
    {
        var text = await _redisStorage.GetTextAsync(id);
        var rank = await _redisStorage.GetRankAsync(id);
        var similarity = await _redisStorage.GetSimilarityAsync(id);

        if (text == null || rank == null || similarity == null)
        {
            return null;
        }

        return _textEvaluationResultBuilder
            .New()
            .WithId(id)
            .WithText(text)
            .WithRank(rank.Value)
            .WithSimilarity(similarity.Value)
            .Build();
    }
}