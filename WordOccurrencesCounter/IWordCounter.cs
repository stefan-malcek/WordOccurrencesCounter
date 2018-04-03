using System.Collections.Generic;

namespace WordOccurrencesCounter
{
    public interface IWordCounter
    {
        Dictionary<string, int> CountOccurences(IEnumerable<string> words);
    }
}
