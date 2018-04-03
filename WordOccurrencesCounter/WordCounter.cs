using System.Collections.Generic;
using System.Linq;

namespace WordOccurrencesCounter
{
    public class WordCounter : IWordCounter
    {
        private readonly IConfiguration _configuration;

        public WordCounter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Dictionary<string, int> CountOccurences(IEnumerable<string> words)
        {
            var occurrences = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (occurrences.ContainsKey(word))
                {
                    occurrences[word]++;
                }
                else
                {
                    occurrences.Add(word, 1);
                }
            }

            return FilterBottomCountBoundary(occurrences).ToDictionary(k => k.Key, v => v.Value);
        }

        private IEnumerable<KeyValuePair<string, int>> FilterBottomCountBoundary(Dictionary<string, int> occurrences)
        {
            return occurrences.Where(w => w.Value >= _configuration.BottomCountBoundary)
                .OrderByDescending(o => o.Value);
        }
    }
}
