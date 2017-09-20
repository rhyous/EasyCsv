using Rhyous.EasyCsv.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rhyous.EasyCsv
{
    public class CsvParserUnescapedQuotes : ICsvParser
    {
        public List<Row<string>> GetRowsFromStream(StreamReader reader, char delimiter = ',')
        {
            var rows = new List<Row<string>>();
            var row = new Row<string>();
            var groupOpen = false;
            var subGroupOpen = false;
            var cellDataStarted = false;
            var builder = new StringBuilder();
            var skipped = new StringBuilder();
            char? lastChar = null;
            int columnCount = int.MaxValue;
            do
            {
                var peekChar = (char)reader.Peek();
                if (!cellDataStarted && peekChar.IsWhiteSpaceButNotNewLine())
                    SkipWhitespace(reader);

                cellDataStarted = true;
                char c = (char)reader.Read();
                if (c == '"')
                {
                    if (reader.Peek() == '"')
                    {
                        SkipNext(reader, new[] { '"' });
                    }
                    else
                    {
                        if (subGroupOpen)
                        {
                            subGroupOpen = !subGroupOpen;
                            goto Append;
                        }
                        groupOpen = !groupOpen;
                        goto Continue;
                    }
                }
                if (!groupOpen && c.IsNewLine())
                {
                    if (rows.Count == 0)
                        columnCount = row.Count;
                    AddRow(rows, ref row, ref builder);
                    SkipNext(reader, "\r\n".ToCharArray(), 100);
                    cellDataStarted = false;
                    skipped.Clear();
                    goto Continue;
                }
                if (c == delimiter && !groupOpen)
                {
                    AddColumn(row, ref builder);
                    cellDataStarted = false;
                    skipped.Clear();
                    goto Continue;
                }
                if (char.IsWhiteSpace(c) && !groupOpen)
                {
                    skipped.Append(c);
                    goto Continue;
                }
                if (skipped.Length > 0)
                {
                    builder.Append(skipped);
                    skipped.Clear();
                }
                if (lastChar == '"' && cellDataStarted && !groupOpen)
                {
                    groupOpen = !groupOpen;
                    subGroupOpen = true;
                    builder.Append(lastChar);
                }           
                Append:
                    builder.Append(c);
                Continue:
                    lastChar = c;
                    continue;

            } while (!reader.EndOfStream);
            AddFinalRow(rows, ref row, ref builder);
            return rows;
        }

        internal static void AddFinalRow(List<Row<string>> rows, ref Row<string> row, ref StringBuilder builder)
        {
            if (row.Count > 0)
            {
                AddRow(rows, ref row, ref builder);
            }
        }

        internal static void SkipWhitespace(StreamReader reader)
        {
            while (char.IsWhiteSpace((char)reader.Peek()))
            {
                reader.Read(); //skip next whitespace
            }
        }

        internal static void SkipNext(StreamReader reader, char[] skipChars, int maxSkips = 1)
        {
            var loopCount = 0;
            while (skipChars.Any(c => c == (char)reader.Peek()) && loopCount < maxSkips)
            {
                reader.Read(); //skip next
                loopCount++;
            }
        }

        internal static void AddRow(List<Row<string>> rows, ref Row<string> row, ref StringBuilder builder)
        {
            if (IsWhiteSpaceOnly(row, builder))
                return;
            AddColumn(row, ref builder);
            rows.Add(row);
            row = new Row<string>();
        }

        internal static void AddColumn(List<string> row, ref StringBuilder builder)
        {
            row.Add(builder.ToString());
            builder = new StringBuilder();
        }

        internal static bool IsWhiteSpaceOnly(List<string> row, StringBuilder builder)
        {
            return row.Count == 0 && string.IsNullOrWhiteSpace(builder.ToString());
        }
    }
}
