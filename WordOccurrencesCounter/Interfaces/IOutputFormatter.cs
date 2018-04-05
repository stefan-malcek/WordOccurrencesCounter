namespace WordOccurrencesCounter.Interfaces
{
    public interface IOutputFormatter
    {
        string FormatHeader();
        string FormatLine(string word, int occurences, decimal percentage);
    }
}
