using System;
using System.Collections.Generic;
using System.IO;

namespace Rhyous.EasyCsv
{
    public abstract class CsvBase : ICsv
    {
        public CsvBase(char delimiter = ',') {            
            Delimiter = delimiter;
        }

        public CsvBase(bool hasHeaderRow, char delimiter = ',') : this(delimiter)
        {
            HasHeaderRow = hasHeaderRow;
        }

        public CsvBase(IEnumerable<string> headers, char delimiter = ',') : this(true, delimiter)
        {
            Headers.AddRange(headers);
        }

        public CsvBase(string csvPath, bool hasHeaderRow = true, char delimiter = ',') : this(hasHeaderRow, delimiter)
        {
            CsvPath = csvPath;
            if (FileExists)
            {
                ParseCsv();
            }
        }

        public virtual char Delimiter { get; }

        public virtual bool HasHeaderRow { get; }

        public virtual bool ThrowExceptionOnMissingHeader { get; set; }

        public virtual int Columns { get { return Headers.Count > 0 ? Headers.Count : (Rows.Count > 0 ? Rows[0].Count : 0); } }

        public virtual bool FileExists
        {
            get { return File.Exists(CsvPath); }
        }

        public virtual string CsvPath { get; protected set; }

        public virtual List<string> Headers
        {
            get { return _Headers.Value; }
        } private readonly Lazy<List<string>> _Headers = new Lazy<List<string>>();

        public virtual RowCollection<string> Rows
        {
            get { return _Rows ?? (_Rows = new RowCollection<string>(this)); }
        } private RowCollection<string> _Rows;

        public abstract void ParseCsv();
    }
}
