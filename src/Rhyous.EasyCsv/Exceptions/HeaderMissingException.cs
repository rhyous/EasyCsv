using System;

namespace Rhyous.EasyCsv
{
    public class HeaderMissingException : Exception
    {
        public HeaderMissingException(string header) : base("The header was not found: " + header) { }
    }
}
