using System;
using System.Globalization;
using WordOccurrencesCounter.Interfaces;

namespace WordOccurrencesCounter
{
    public class CsvFileFormatter : IOutputFormatter
    {
        public string FormatHeader()
        {
            return $"{Program.Word};{Program.Occurrences};{Program.Percentage},";
        }

        public string FormatLine(string word, int occurences, decimal percentage)
        {
            return $"{word};{occurences};{percentage.ToString(CultureInfo.InvariantCulture)},";
        }
    }
}
