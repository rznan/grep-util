using System.Text.RegularExpressions;

namespace Grep.Core.SearchEngine;

public class RegexSearch : BaseSearchEngine
{
    public override bool LineMatch(string line, string target)
    {
        return Regex.IsMatch(line, target);
    }
}
