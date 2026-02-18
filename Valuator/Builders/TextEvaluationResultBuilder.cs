using Valuator.Models;

namespace Valuator.Builders
{
    public class TextEvaluationResultBuilder : ITextEvaluationResultBuilder
    {
        #region Initialization
        private TextEvaluationResult _result;

        public ITextEvaluationResultBuilder New()
        {
            _result = new TextEvaluationResult(
                    id: string.Empty,
                    text: string.Empty,
                    rank: 0.0,
                    similarity: 0.0
                );

            return this;
        }
        public ITextEvaluationResultBuilder FromExisting(TextEvaluationResult result)
        {
            _result = result;
            return this;
        }
        #endregion

        #region Properties
        public ITextEvaluationResultBuilder WithId(string id)
        {
            _result.Id = id;
            return this;
        }

        public ITextEvaluationResultBuilder WithText(string text)
        {
            _result.Text = text;
            return this;
        }

        public ITextEvaluationResultBuilder WithRank(double rank)
        {
            _result.Rank = rank;
            return this;
        }

        public ITextEvaluationResultBuilder WithSimilarity(double similarity)
        {
            _result.Similarity = similarity;
            return this;
        }
        #endregion

        #region Result
        public TextEvaluationResult Build()
        {
            Validate();
            return _result;
        }

        public TextEvaluationResult GetResult()
        {
            return _result;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(_result.Id))
            {
                throw new InvalidOperationException("Id не может быть пустым");
            }

            if (string.IsNullOrWhiteSpace(_result.Text))
            {
                throw new InvalidOperationException("Text не может быть пустым");
            }

            if (_result.Rank < 0 || _result.Rank > 1)
            {
                throw new InvalidOperationException("Rank должен быть в диапазоне от 0 до 1");
            }

            if (_result.Similarity != 0 && _result.Similarity != 1)
            {
                throw new InvalidOperationException("Similarity может быть только 0 или 1");
            }
        }
        #endregion
    }
}
