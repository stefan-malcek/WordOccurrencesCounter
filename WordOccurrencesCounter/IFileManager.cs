using System.Collections.Generic;

namespace WordOccurrencesCounter
{
    public interface IFileManager
    {
        IEnumerable<string> LoadWords();
        void WriteResult(Dictionary<string, int> occurrences);
    }
}
