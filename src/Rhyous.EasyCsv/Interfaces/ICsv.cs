using System.Collections.Generic;

namespace Rhyous.EasyCsv
{
    public interface ICsv
    {
        char Delimiter { get; }
        bool HasHeaderRow { get; }
        /// <summary>
        /// If header doesn't exist, throw an excpetion. Otherwise the expectation is null
        /// or in primitives, default(T).
        /// </summary>
        bool ThrowExceptionOnMissingHeader { get; set; }
        bool FileExists { get; }
        string CsvPath { get; }
        List<string> Headers { get; }
        int Columns { get; }
        RowCollection<string> Rows { get; }

        void ParseCsv();
    }
}
