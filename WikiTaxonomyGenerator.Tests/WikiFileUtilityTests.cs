using Xunit;

namespace TaxonomyGenerator.Tests
{
    public class WikiFileUtilityTests
    {
        [Fact]
        public void CreateFileName_RemovesBackslashes()
        {
            var input = "\\";
            var output = WikiFileUtility.CreateFileName(input);
            Assert.Equal(string.Empty, output);
        }

        [Fact]
        public void CreateFileName_ReplacesColons()
        {
            var input = ":";
            var output = WikiFileUtility.CreateFileName(input);
            Assert.Equal("%3A", output);
        }

        [Fact]
        public void CreateFileName_ReplacesSpaces()
        {
            var input = " ";
            var output = WikiFileUtility.CreateFileName(input);
            Assert.Equal("-", output);
        }

        [Fact]
        public void CreateTitle_ReplacesDashes()
        {
            var input = "This-is-a-title";
            var output = WikiFileUtility.CreateTitle(input);
            Assert.Equal("This is a title", output);
        }

        [Fact]
        public void CreateTitle_DoesNotReverse_CreateFileName()
        {
            var input = "The world is b-e-a-utiful!";
            var output = TaxonomyGenerator.WikiFileUtility.CreateTitle(TaxonomyGenerator.WikiFileUtility.CreateFileName(input));
            Assert.NotEqual(output, input);
        }
    }
}
