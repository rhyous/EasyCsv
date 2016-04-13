using System;
using System.Collections.Generic;

namespace Rhyous.EasyCsv
{
    public interface ICsv
    {
        char Delimiter { get; }
        bool HasHeader { get; }
        bool FileExists { get; }
        string CsvPath { get; }
        List<string> Headers { get; }
        int Columns { get; }
        List<List<string>> Rows { get; }

        void ParseCsv();
    }
}
