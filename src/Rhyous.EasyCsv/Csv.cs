﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rhyous.EasyCsv
{
    public class Csv : CsvBase
    {
        private readonly Stream _Stream;

        public Csv(string csvPath, bool hasHeaderLine = true, char delimiter = ',') : base(csvPath, hasHeaderLine, delimiter)
        {
        }

        public Csv(string csvPath, char delimiter)
            : this(csvPath, true, delimiter)
        {
        }

        public Csv(Stream stream, bool hasHeaderLine = true, char delimiter = ',')
            : this("", hasHeaderLine, delimiter)
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
                if (HasHeader)
                {
                    Headers.AddRange(rows[0]);
                }
                Rows.AddRange(HasHeader ? rows.Skip(1) : rows);
                foreach (var row in Rows)
                {
                    row.Parent = this;
                }
            }
        }

        private List<Row<string>> GetRows()
        {
            if (!FileExists)
                return null;
            var rows = new List<Row<string>>();
            var row = new Row<string>();
            var groupOpen = false;
            var cellDataStarted = false;
            var builder = new StringBuilder();
            var skipped = new StringBuilder();
            var reader = _Stream == null ? new StreamReader(CsvPath) : new StreamReader(_Stream);
            do
            {
                if (!cellDataStarted && char.IsWhiteSpace((char)reader.Peek()))
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
                        groupOpen = !groupOpen;
                        continue;
                    }
                }
                if (!groupOpen && (c == '\r' || c == '\n'))
                {
                    AddRow(rows, ref row, ref builder);
                    SkipNext(reader, "\r\n".ToCharArray(), 100);
                    cellDataStarted = false;
                    skipped.Clear();
                    continue;
                }
                if (c == Delimiter && !groupOpen)
                {
                    AddColumn(row, ref builder);
                    cellDataStarted = false;
                    skipped.Clear();
                    continue;
                }
                if (char.IsWhiteSpace(c) && !groupOpen)
                {
                    skipped.Append(c);
                    continue;
                }
                if (skipped.Length > 0)
                {
                    builder.Append(skipped);
                    skipped.Clear();
                }
                builder.Append(c);
            } while (!reader.EndOfStream);
            AddFinalRow(rows, ref row, ref builder);
            return rows;
        }

        private static void AddFinalRow(List<Row<string>> rows, ref Row<string> row, ref StringBuilder builder)
        {
            if (row.Count > 0)
            {
                AddRow(rows, ref row, ref builder);
            }
        }

        private static void SkipWhitespace(StreamReader reader)
        {
            while (char.IsWhiteSpace((char)reader.Peek()))
            {
                reader.Read(); //skip next whitespace
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

        private static void AddRow(List<Row<string>> rows, ref Row<string> row, ref StringBuilder builder)
        {
            if (IsWhiteSpaceOnly(row, builder))
                return;
            AddColumn(row, ref builder);
            rows.Add(row);
            row = new Row<string>();
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
