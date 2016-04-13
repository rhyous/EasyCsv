using System.IO;
using System.Text;

namespace Rhyous.EasyCsv.Tests.Extensions
{
    public static class StringExtensions
    {
        public static Stream AsStream(this string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value));
        }
    }
}
