namespace Valuator.Models
{
    public class TextEvaluationResult
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public double Rank { get; set; }
        public double Similarity { get; set; }

        public TextEvaluationResult(
            string id,
            string text,
            double rank,
            double similarity
        )
        {
            this.Id = id;
            this.Text = text;
            this.Rank = rank;
            this.Similarity = similarity;
        }
    }
}
