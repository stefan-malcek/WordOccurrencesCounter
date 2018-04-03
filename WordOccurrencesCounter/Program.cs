namespace WordOccurrencesCounter
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            IConfiguration configuration = new Configuration();
            IFileManager fileManager = new FileManager(configuration);
            var words = fileManager.LoadWords();

            IWordCounter wordCounter = new WordCounter(configuration);
            var occurrences = wordCounter.CountOccurences(words);

            fileManager.WriteResult(occurrences);
        }
    }
}
