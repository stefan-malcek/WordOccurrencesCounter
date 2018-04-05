using System.Collections.Generic;

namespace WordOccurrencesCounter.Interfaces
{
    public interface IFileManager
    {
        bool TryToLoadStopWords(string file, out IEnumerable<string> stopwords);
        IEnumerable<string> LoadWords(string file, IEnumerable<string> stopwords);
        void WriteResult(string file, Dictionary<string, int> occurrences);
    }
}
