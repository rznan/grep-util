namespace Grep.Core.SearchEngine;

public class BasicSearch : BaseSearchEngine
{
    public override bool LineMatch(string line, string target)
    {
        return line.Contains(target);
    }
}
