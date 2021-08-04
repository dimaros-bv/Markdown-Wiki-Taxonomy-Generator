using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TaxonomyGenerator
{
    class TaxonomyWriter
    {
        private readonly string root;
        private readonly IFormatLocations formatter;

        public TaxonomyWriter(string root, IFormatLocations formatter)
        {
            this.root = root;
            this.formatter = formatter;
        }

        public void Write(Dictionary<string, SortedSet<string>> taxonomyLink)
        {
            foreach (var taxonomy in taxonomyLink.Keys)
            {
                string filePath = Path.Join(root, WikiFileUtility.CreateFileName(taxonomy) + ".md");
                CreateDirectoriesIfNotExist(filePath);

                string index = CreateText(taxonomyLink[taxonomy]);
                File.WriteAllText(filePath, index);
            }
        }

        private string CreateText(SortedSet<string> locations)
            => string.Join(Environment.NewLine, locations.Select(formatter.Format));

        private void CreateDirectoriesIfNotExist(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            Directory.CreateDirectory(directoryPath);
        }
    }
}
