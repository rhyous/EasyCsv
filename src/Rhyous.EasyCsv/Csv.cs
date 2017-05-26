using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rhyous.EasyCsv
{
    public class Csv : CsvBase
    {
        private readonly Stream _Stream;

        public Csv() : base(false, ',') { }
        public Csv(char delimiter) : base(false, delimiter) { }
        public Csv(bool hasHeaderRow, char delimiter = ',') : base(hasHeaderRow, delimiter) { }
        public Csv(List<string> headers, char delimiter = ',') : base(headers, delimiter) { }
        public Csv(string csvPath, bool hasHeaderRow = true, char delimiter = ',') : base(csvPath, hasHeaderRow, delimiter) { }
        public Csv(string csvPath, char delimiter) : this(csvPath, true, delimiter) { }

        public Csv(Stream stream, bool hasHeaderRow = true, char delimiter = ',')
            : this("", hasHeaderRow, delimiter)
        {
            _Stream = stream;
            if (FileExists)
            {
                ParseCsv();
            }
        }

        public Csv(Stream stream, char delimiter)
            : this(stream, true, delimiter)
        {
        }

        public ICsvParser Parser
        {
            get { return _Parser ?? (_Parser = new CsvParser()); }
            internal set { _Parser = value; }
        } private ICsvParser _Parser;


        public override bool FileExists
        {
            get { return base.FileExists || _Stream != null; }
        }

        public override void ParseCsv()
        {
            Clear();
            var rows = GetRows();
            if (rows.Count > 0)
            {
                if (HasHeaderRow)
                {
                    Headers.AddRange(rows[0]);
                }
                Rows.AddRange(HasHeaderRow ? rows.Skip(1) : rows);
                foreach (var row in Rows)
                {
                    row.Parent = this;
                }
            }
        }

        internal List<Row<string>> GetRows()
        {
            if (!FileExists)
                return null;
            var reader = _Stream == null ? new StreamReader(CsvPath) : new StreamReader(_Stream);
            return Parser.GetRowsFromStream(reader, Delimiter);
        }

        private void Clear()
        {
            Headers.Clear();
            Rows.Clear();
        }
    }
}
