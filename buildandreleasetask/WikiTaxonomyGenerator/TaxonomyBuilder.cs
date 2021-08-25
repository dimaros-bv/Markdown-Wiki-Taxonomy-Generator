using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TaxonomyGenerator
{
    class TaxonomyBuilder
    {
        private readonly string rootPath;
        private readonly Options options;
        private readonly IParseLinks parser;
        private Dictionary<string, SortedSet<string>> taxonomyLinks;

        /// <summary>
        /// </summary>
        /// <param name="rootPath">Path without trailing slash</param>
        /// <param name="options"></param>
        public TaxonomyBuilder(string rootPath, Options options, IParseLinks parser)
        {
            this.rootPath = rootPath;
            this.options = options;
            this.parser = parser;
        }

        public Dictionary<string, SortedSet<string>> Build(string root)
        {
            taxonomyLinks = new Dictionary<string, SortedSet<string>>();

            this.Traverse(root);
            return taxonomyLinks;
        }

        public void Traverse(string root)
        {
            foreach (var file in Directory.GetFiles(root, "*.md"))
                AppendToTaxonomy(file);

            foreach (var dir in Directory.GetDirectories(root))
                if (dir != options.PathToTaxonomy)
                    Traverse(dir);
        }

        private void AppendToTaxonomy(string pathToFile)
        {
            var linkedTaxonomies = ReadTaxonomyHeader(pathToFile).ToArray();
            var location = RelativePath(pathToFile).Replace('\\', '/');

            foreach (var taxonomy in linkedTaxonomies)
            {
                if (!taxonomyLinks.ContainsKey(taxonomy))
                    taxonomyLinks.Add(taxonomy, new SortedSet<string>());

                taxonomyLinks[taxonomy].Add(location);
            }
        }

        private IEnumerable<string> ReadTaxonomyHeader(string pathToFile)
            => File
                .ReadAllLines(pathToFile)
                .SkipWhile(s => s != options.HeaderStart).Skip(1)
                .TakeWhile(s => s != options.HeaderEnd)
                .SelectMany(line => parser.ParseLine(line))
                .Where(url => url.StartsWith(options.PathToTaxonomy));

        private string RelativePath(string pathToFile) => pathToFile.Substring(rootPath.Length);
    }
}
