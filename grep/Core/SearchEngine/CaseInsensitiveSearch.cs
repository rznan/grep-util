namespace Grep.Core.SearchEngine;

public class CaseInsensitiveSearch : BaseSearchEngine
{
    public override bool LineMatch(string line, string target)
    {
        return line.ToLower().Contains(target.ToLower());
    }
}
