using System;
using System.Collections.Generic;
using System.Configuration;

namespace WordOccurrencesCounter
{
    public class Configuration : IConfiguration
    {
        public string InputFile { get; }
        public string OutputFileName { get; }
        public List<string> IgnoredWords { get; }
        public int BottomCountBoundary { get; }
        public char Separator { get; }

        public Configuration()
        {
            InputFile = ConfigurationManager.AppSettings["InputFile"];
            OutputFileName = ConfigurationManager.AppSettings["OutputFileName"];
            IgnoredWords = new List<string>();
            BottomCountBoundary = int.Parse(ConfigurationManager.AppSettings["BottomCountBoundary"]);
            Separator = Convert.ToChar(ConfigurationManager.AppSettings["Separator"]);
        }
    }
}
