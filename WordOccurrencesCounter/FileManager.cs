using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordOccurrencesCounter
{
    public class FileManager : IFileManager
    {
        private readonly IConfiguration _configuration;

        public FileManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<string> LoadWords()
        {
            var text = File.ReadAllText(_configuration.InputFile);
            return text.Split(_configuration.Separator).Where(w => !_configuration.IgnoredWords.Contains(w));
        }

        public void WriteResult(Dictionary<string, int> occurrences)
        {
            using (var stream = new FileStream($"{_configuration.OutputFileName}.csv", FileMode.Create))
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine("Word;Occurrences;Percentage");

                var totalWordsCount = occurrences.Sum(s => s.Value);
                foreach (var occurrence in occurrences)
                {
                    writer.WriteLine($"{occurrence.Key};{occurrence.Value};{(double)occurrence.Value / totalWordsCount * 100:##.###}");
                }
            }
        }
    }
}
