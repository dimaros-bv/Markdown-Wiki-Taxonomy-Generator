using Xunit;

namespace TaxonomyGenerator.Tests
{
    public class LinkParserTests
    {
        [Fact]
        public void FindsSimpleLink()
        {
            var url = "/Taxonomy/Subdirectory";
            var line = $"Front matter: [Some Title]({url})";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Single(output);
            Assert.Equal(output[0], url);
        }

        [Fact]
        public void FindsMultipleLinks()
        {
            var urls = new[] {
                "/Taxonomy/Subdirectory",
                "/Taxonomy/Other Directory"
            };

            var line = $"Front matter: [Some Title]({urls[0]}), [Some Other Title]({urls[1]})";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Equal(output, urls);
        }

        [Fact]
        public void FindsLinksWithParenthesesInUrl()
        {
            var url = "/Taxonomy/Subdirectory (With Parens)";
            var line = $"Front matter: [Some Title]({url})";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Single(output);
            Assert.Equal(output[0], url);
        }

        [Fact]
        public void FindsLinksWithBracketsInText()
        {
            var url = "/Taxonomy/Subdirectory (With Parens)";
            var line = $"Front matter: [[tag] Some Title]({url})";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Single(output);
            Assert.Equal(output[0], url);
        }

        [Fact]
        public void IgnoresLinksWithEmptyText()
        {
            var line = $"[](/Taxonomy)";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Empty(output);
        }

        [Fact]
        public void IgnoresLinksWithEmptyUrl()
        {
            var line = $"[My Link]()";
            var parser = new MarkdownParser();

            var output = parser.ParseLine(line);

            Assert.Empty(output);
        }
    }
}
