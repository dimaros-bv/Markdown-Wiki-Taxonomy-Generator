using System.Linq;
using System.Text.RegularExpressions;

namespace TaxonomyGenerator
{
    public interface IParseLinks
    {
        string[] ParseLine(string line);
    }

    public class MarkdownParser : IParseLinks
    {
        // \\[(.*?)\\]                                              Match link-text between square parentheses
        //            \\(                                           Match open parenthesis
        //               (?<url>                                    Start named capture group 'url'
        //                      (                                   Start unnamed capture group †
        //                       (?<BR>\()                          Start named capture group 'BR' matching open parenthesis
        //                                |(?<-BR>\\))              Or match with balancing group matching closing parenthesis
        //                                            |[^()]+       Or match zero or more non-parenthesis characters
        //                                                   )+     Match groups within unnamed capture group † at least once
        //                                                     \\)  Match closing parenthesis
        private static readonly Regex linkRegex = new Regex("\\[(.+?)\\]\\((?<url>((?<BR>\\()|(?<-BR>\\))|[^()]+)+)\\)");
        public string[] ParseLine(string line)
            => linkRegex.Matches(line)
            .Select(match => match.Groups["url"].Value)
            .ToArray();
    }
}
