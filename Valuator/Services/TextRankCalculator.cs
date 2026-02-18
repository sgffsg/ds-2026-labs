namespace Valuator.Services;

public interface ITextRankCalculator
{
    double Calculate(string text);
}

public class TextRankCalculator : ITextRankCalculator
{
    public double Calculate(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0.0;

        double nonLetters = text.Count(c => !char.IsLetter(c));
        return nonLetters / text.Length;
    }
}