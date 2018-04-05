using System;
using System.Linq;

namespace WordOccurrencesCounter.Helpers
{
    public static class StringExtensions
    {
        public static string RemovePunctuation(this string word)
        {
            return new string(word.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        public static bool IsEqualsTo(this string word1, string word2)
        {
            return string.Equals(word1, word2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
