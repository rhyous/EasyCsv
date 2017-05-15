using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Rhyous.EasyCsv
{
    public class NaiveCsv : CsvBase
    {
        public NaiveCsv(string csvPath, bool hasHeaderLine = true, char delimiter = ',')
            : base(csvPath, hasHeaderLine, delimiter)
        {
            Lines.AddRange(GetLines());
        }

        public NaiveCsv(string csvPath, char delimiter)
            : this(csvPath, true, delimiter)
        {
        }

        private List<string> GetHeaders(List<string> lines)
        {
            var headers = new List<string>();
            if (HasHeaderRow && lines.Count > 0)
            {
                headers = GetColumnsFromLine(lines[0]);
            }
            return headers;
        }

        public readonly List<string> Lines = new List<string>();

        private List<Row<string>> GetRows(List<string> lines)
        {
            return lines.Skip(HasHeaderRow ? 1 : 0).Select(l => GetColumnsFromLine(l)).ToList();
        }

        private Row<string> GetColumnsFromLine(string line)
        {
            return new Row<string>(line.Split(Delimiter).Select(s => s.Trim()).ToList(), this);
        }

        private List<string> GetLines()
        {
            return FileExists ? File.ReadAllLines(CsvPath).ToList() : null;
        }
        
        public override void ParseCsv()
        {
            var lines = GetLines();
            Headers.AddRange(GetHeaders(lines));
            Rows.AddRange(GetRows(lines));
        }
    }
}
