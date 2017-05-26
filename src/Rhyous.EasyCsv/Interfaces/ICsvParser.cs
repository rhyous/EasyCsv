using System.Collections.Generic;
using System.IO;

namespace Rhyous.EasyCsv
{
    public interface ICsvParser
    {
        List<Row<string>> GetRowsFromStream(StreamReader reader, char delimiter = ',');
    }
}