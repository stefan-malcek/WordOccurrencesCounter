using System.Collections.Generic;

namespace WordOccurrencesCounter
{
    public interface IConfiguration
    {
        string InputFile { get; }
        string OutputFileName { get; }
        IEnumerable<string> IgnoredWords { get; }
        int BottomCountBoundary { get; }
        char Separator { get; }
    }
}
