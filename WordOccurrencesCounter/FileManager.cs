using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WordOccurrencesCounter.Helpers;
using WordOccurrencesCounter.Interfaces;

namespace WordOccurrencesCounter
{
    public class FileManager : IFileManager
    {
        private readonly IOutputFormatter _formatter;

        public FileManager(IOutputFormatter formatter)
        {
            _formatter = formatter;
        }

        public bool TryToLoadStopWords(string file, out IEnumerable<string> stopwords)
        {
            if (!File.Exists(file))
            {
                stopwords = new List<string>();
                return false;
            }

            stopwords = File.ReadAllLines(file).Select(l => l.Trim(' ')).ToList();
            return true;
        }

        public IEnumerable<string> LoadWords(string file, IEnumerable<string> stopWords)
        {
            var text = File.ReadAllText(file);
            return text.Split(' ', '\n', '\r', '\t')
                .Select(w => w.RemovePunctuation())
                .Where(w => !stopWords.Contains(w) && !string.IsNullOrEmpty(w));
        }

        public void WriteResult(string file, Dictionary<string, int> occurrences)
        {
            using (var stream = new FileStream(file, FileMode.Create))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine(_formatter.FormatHeader());

                var totalWordsCount = occurrences.Sum(s => s.Value);
                foreach (var occurrence in occurrences)
                {
                    var percentage = Math.Round((decimal)occurrence.Value / totalWordsCount * 100, 3);
                    writer.WriteLine(_formatter.FormatLine(occurrence.Key, occurrence.Value, percentage));
                }
            }
        }
    }
}
