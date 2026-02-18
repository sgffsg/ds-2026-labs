using Valuator.Models;

namespace Valuator.Services.Interfaces
{
    public interface ITextEvaluationService
    {
        Task<string> EvaluateAsync(string text);
        Task<TextEvaluationResult?> GetResultAsync(string id);
    }
}
