using System.Text.RegularExpressions;
using System.Web;

namespace TaxonomyGenerator
{
    public static class WikiFileUtility
    {
        public static string CreateFileName(string link)
            // Not all "unsafe url" characters are encoded.
            // Therefore we only replace specific characters.
            => link
                .Replace(@"\", "")
                .Replace(":", "%3A")
                .Replace(" ", "-");


        public static string CreateTitle(string filename)
            => HttpUtility.UrlDecode(filename.Replace('-', ' '));
    }
}
