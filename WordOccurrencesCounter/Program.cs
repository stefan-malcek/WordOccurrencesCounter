using System;
using System.Collections.Generic;
using WordOccurrencesCounter.Helpers;
using WordOccurrencesCounter.Interfaces;

namespace WordOccurrencesCounter
{
    internal class Program
    {
        public const string Word = "Word";
        public const string Occurrences = "Occurrences";
        public const string Percentage = "Percentage";
        public const string Txt = "TXT";
        public const string Csv = "CSV";

        internal static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Invalid number of parameters.");
                Console.WriteLine("Example: data.txt result CSV 0");
                Environment.Exit(87);
            }

            var inputFile = args[0];
            var outputFileName = args[1];
            var outputfileFormat = args[2];
            if (!Txt.IsEqualsTo(outputfileFormat) && !Csv.IsEqualsTo(outputfileFormat))
            {
                Console.WriteLine($"Invalid output file format {outputfileFormat}.");
                Console.WriteLine($"Correct formats are {Txt} or {Csv}.");
                Environment.Exit(1);
            }

            PrintConfiguration(args);

            var bottomCountBoundaryParsed = int.TryParse(args[3], out var bottomCountBoundary);
            if (!bottomCountBoundaryParsed || bottomCountBoundary < 0)
            {
                Console.WriteLine("Invalid bottom boundary of occurrences. The value will be set to 0.\n");
                bottomCountBoundary = 0;
            }

            IOutputFormatter formatter;
            if (Csv.IsEqualsTo(outputfileFormat))
                formatter = new CsvFileFormatter();
            else
                formatter = new TextFileFormatter();

            IFileManager fileManager = new FileManager(formatter);
            Console.WriteLine(fileManager.TryToLoadStopWords("stopwords.txt", out var stopwords)
                ? "Stopwords loaded.\n"
                : "Could not load stopwords. The program will not remove any words.\n");

            var words = new List<string>();
            Console.WriteLine("Loading words...");
            try
            {
                words.AddRange(fileManager.LoadWords(inputFile, stopwords));
                Console.WriteLine("Loading finished.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to load {inputFile}.");
                Console.WriteLine(ex.Message);
                Environment.Exit(2);
            }

            IWordCounter wordCounter = new WordCounter();
            Console.WriteLine("Counting occurrenes has begin.");
            var occurrences = wordCounter.CountOccurrences(words, bottomCountBoundary);
            Console.WriteLine("Counting finished.\n");

            Console.WriteLine("Generating csv file.");
            var outputFile = $"{outputFileName}.{outputfileFormat.ToLower()}";
            try
            {
                fileManager.WriteResult(outputFile, occurrences);
                Console.WriteLine("Writing finished.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to write results to {outputFile}.\n");
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

        private static void PrintConfiguration(IReadOnlyList<string> args)
        {
            Console.WriteLine("Loaded configuration");
            Console.WriteLine($"Input file: {args[0]}");
            Console.WriteLine($"Output file name: {args[1]}");
            Console.WriteLine($"Output file format: {args[2]}");
            Console.WriteLine($"Print words with occurrences more than: {args[3]}");
        }
    }
}
