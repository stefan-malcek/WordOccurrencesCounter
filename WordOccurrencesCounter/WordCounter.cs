using System.Collections.Generic;
using System.Linq;
using WordOccurrencesCounter.Interfaces;

namespace WordOccurrencesCounter
{
    public class WordCounter : IWordCounter
    {
        public Dictionary<string, int> CountOccurrences(IEnumerable<string> words, int bottomCountBoundary)
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

            return FilterBottomOccurrencesBoundary(occurrences, bottomCountBoundary)
                .ToDictionary(k => k.Key, v => v.Value);
        }

        private static IEnumerable<KeyValuePair<string, int>> FilterBottomOccurrencesBoundary(Dictionary<string, int> occurrences,
            int bottomCountBoundary)
        {
            return occurrences.Where(w => w.Value >= bottomCountBoundary).OrderByDescending(o => o.Value);
        }
    }
}
