using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rhyous.EasyCsv
{
    public class Csv : CsvBase
    {
        public Csv(string csvPath, bool hasHeaderLine = true, char delimiter = ',') : base(csvPath, hasHeaderLine, delimiter)
        {
        }

        public Csv(string csvPath, char delimiter)
            : this(csvPath, true, delimiter)
        {
        }

        public override void ParseCsv()
        {
            Clear();
            var rows = GetRows();          
            if (rows.Count > 0)
            {
                if (HasHeader)
                {
                    Headers.AddRange(rows[0]);
                }
                Rows.AddRange(HasHeader ? rows.Skip(1) : rows);
            }
        }

        private List<List<string>> GetRows()
        {
            var rows = new List<List<string>>();
            var row = new List<string>();
            var groupOpen = false;
            var builder = new StringBuilder();
            var reader = new StreamReader(CsvPath);
            do
            {
                char c = (char)reader.Read();
                if (c == '"')
                {
                    if (reader.Peek() == '"')
                    {
                        SkipNext(reader, new[] { '"' });
                    }
                    else
                    {
                        groupOpen = !groupOpen;
                        continue;
                    }
                }
                if (!groupOpen && (c == '\r' || c == '\n'))
                {
                    AddRow(rows, ref row, ref builder);
                    SkipNext(reader, "\r\n".ToCharArray(), 100);
                    continue;
                }
                if (c == Delimiter && !groupOpen)
                {
                    AddColumn(row, ref builder);
                    continue;
                }
                builder.Append(c);
            } while (!reader.EndOfStream);
            AddFinalRow(rows, ref row, ref builder);
            return rows;
        }

        private static void AddFinalRow(List<List<string>> rows, ref List<string> row, ref StringBuilder builder)
        {
            if (row.Count > 0)
            {
                AddRow(rows, ref row, ref builder);
            }
        }

        private static void SkipNext(StreamReader reader, char[] skipChars, int maxSkips = 1)
        {
            var loopCount = 0;
            while (skipChars.Any(c => c == (char)reader.Peek()) && loopCount < maxSkips)
            {
                reader.Read(); //skip next
                loopCount++;
            }
        }

        private static void AddRow(List<List<string>> rows, ref List<string> row, ref StringBuilder builder)
        {
            if (IsWhiteSpaceOnly(row, builder))
                return;
            AddColumn(row, ref builder);
            rows.Add(row);
            row = new List<string>();
        }

        private static void AddColumn(List<string> row, ref StringBuilder builder)
        {
            row.Add(builder.ToString());
            builder = new StringBuilder();
        }

        private static bool IsWhiteSpaceOnly(List<string> row, StringBuilder builder)
        {
            return row.Count == 0 && string.IsNullOrWhiteSpace(builder.ToString());
        }

        private void Clear()
        {
            Headers.Clear();
            Rows.Clear();
        }
    }
}
