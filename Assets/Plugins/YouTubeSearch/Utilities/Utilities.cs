using System.Net;

namespace YouTubeSearch
{
    internal class Utilities
    {
        public static string HtmlDecode(string value)
        {
            try
            {
                return WebUtility.HtmlDecode(value);
            }
            catch
            {
                return value;
            }
        }
    }
}