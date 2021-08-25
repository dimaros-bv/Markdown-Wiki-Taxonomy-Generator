using System.IO;

namespace TaxonomyGenerator
{
    public interface IFormatLocations
    {
        string Format(string location);
    }

    public class ListFormatter : IFormatLocations
    {
        public string Format(string location)
        {
            var fileName = Path.GetFileName(location);
            var extension = Path.GetExtension(location);

            var title = WikiFileUtility.CreateTitle(fileName);

            // Cannot use Path.GetFileNameWithoutExtension, because decoded path might not be a valid path anymore.
            var titleWithoutExtension = title.Substring(0, title.Length - extension.Length);

            return $"* [{titleWithoutExtension}]({location})";
        }
    }
}
