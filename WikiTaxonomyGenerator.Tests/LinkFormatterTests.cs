using Xunit;

namespace TaxonomyGenerator.Tests
{
    public class LinkFormatterTests
    {
        const string FORMAT = "* [{0}]({1})";

        [Fact]
        public void StripsExtension()
        {
            var location = "test.md";
            var formatter = new ListFormatter();

            var output = formatter.Format(location);

            Assert.Equal(string.Format(FORMAT, "test", location), output);
        }

        [Fact]
        public void StripsDirectories()
        {
            var location = "/relative/path/to/test.md";
            var formatter = new ListFormatter();

            var output = formatter.Format(location);

            Assert.Equal(string.Format(FORMAT, "test", location), output);
        }
    }
}
