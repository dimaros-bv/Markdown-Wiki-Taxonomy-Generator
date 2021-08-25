using System;

namespace TaxonomyGenerator
{
    class Program
    {
        static void Main(string root, string headerStart, string headerEnd = null, string taxonomyPath = "/Taxonomy")
        {
            var options = new Options
            {
                WikiRoot = root.TrimEnd('/'),
                HeaderStart = headerStart,
                HeaderEnd = headerEnd ?? headerStart,
                PathToTaxonomy = taxonomyPath
            };

            var tax = new TaxonomyBuilder(
                options.WikiRoot,
                options,
                new MarkdownParser());

            var taxonomyLinks = tax.Build(options.WikiRoot);

            new TaxonomyWriter(options.WikiRoot, new ListFormatter())
                .Write(taxonomyLinks);
        }
    }

    public class Options
    {
        public string PathToTaxonomy { get; set; } = "Taxonomy";
        public string HeaderStart { get; set; }
        public string HeaderEnd { get; set; }
        public string WikiRoot { get; set; }
    }
}
