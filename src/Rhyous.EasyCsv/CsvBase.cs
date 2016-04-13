using System;
using System.Collections.Generic;
using System.IO;

namespace Rhyous.EasyCsv
{
    public abstract class CsvBase : ICsv
    {
        public CsvBase(string csvPath, bool hasHeaderLine = true, char delimiter = ',')
        {
            CsvPath = csvPath;
            HasHeader = hasHeaderLine;
            Delimiter = delimiter;
            if (FileExists)
            {
                ParseCsv();
            }
        }

        public virtual char Delimiter { get; }

        public virtual bool HasHeader { get; }

        public virtual int Columns { get { return Headers.Count > 0 ? Headers.Count : (Rows.Count > 0 ? Rows[0].Count : 0); } }

        public virtual bool FileExists
        {
            get { return File.Exists(CsvPath); }
        }

        public virtual string CsvPath { get; }

        public virtual List<string> Headers
        {
            get { return _Headers.Value; }
        } private readonly Lazy<List<string>> _Headers = new Lazy<List<string>>();

        public virtual List<List<string>> Rows
        {
            get { return _Rows.Value; }
        } private readonly Lazy<List<List<string>>> _Rows = new Lazy<List<List<string>>>();

        public abstract void ParseCsv();
    }
}
