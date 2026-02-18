using Valuator.Models;

namespace Valuator.Builders
{
    public interface ITextEvaluationResultBuilder
    {
        #region Initialization
        public ITextEvaluationResultBuilder New();
        public ITextEvaluationResultBuilder FromExisting(TextEvaluationResult result);
        #endregion

        #region Properties
        public ITextEvaluationResultBuilder WithId(string id);
        public ITextEvaluationResultBuilder WithText(string text);
        public ITextEvaluationResultBuilder WithRank(double rank);
        public ITextEvaluationResultBuilder WithSimilarity(double similarity);
        #endregion

        #region Result
        public TextEvaluationResult Build();
        public TextEvaluationResult GetResult();

        void Validate();
        #endregion
    }
}
