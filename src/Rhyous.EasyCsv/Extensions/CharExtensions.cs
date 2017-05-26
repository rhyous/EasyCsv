namespace Rhyous.EasyCsv.Extensions
{
    public static class CharExtensions
    {
        public static bool IsNewLine(this char c)
        {
            return c == '\r' || c == '\n';
        }

        public static bool IsWhiteSpaceButNotNewLine(this char c)
        {
            return char.IsWhiteSpace(c) && !c.IsNewLine();
        }
    }
}
