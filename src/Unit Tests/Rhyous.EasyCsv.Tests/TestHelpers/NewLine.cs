using System.IO;
using System.Text;

namespace Rhyous.EasyCsv.Tests
{
    /// <summary>
    /// After checking in the code to GitHub, the lines are \n.
    /// After checking out the code from GitHub to a windows machine
    /// the lines are \r\n.    
    /// Build systems may check out new lines either way. So the tests
    /// need to know which character(s) the end of line consists of.
    /// So I created a file that is just a sigle new line.
    /// </summary>
    public class NewLine
    {
        public static string NewLineFile = @"Data\NewLine.txt";

        public static string Get()
        {
            return GetNewLineFromFile(NewLineFile);
        }

        public static string GetNewLineFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var reader = new StreamReader(fileName);
                var builder = new StringBuilder();
                do
                {
                    builder.Append((char)reader.Read());

                } while (!reader.EndOfStream);
                return builder.ToString();
            }
            throw new FileNotFoundException("File not found.", fileName);
        }
    }
}
