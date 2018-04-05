using System.Collections.Generic;

namespace WordOccurrencesCounter.Interfaces
{
    public interface IWordCounter
    {
        Dictionary<string, int> CountOccurrences(IEnumerable<string> words, int bottomCountBoundary);
    }
}
