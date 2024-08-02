namespace Grep.Core.SearchEngine;

public class CaseInsensitiveSearch : BaseSearchEngine
{
    public override bool LineMatch(string line, string target)
    {
        return line.ToLower().Contains(target);
    }

    public override string Search(FileReader fr, string target)
    {
        return base.Search(fr, target.ToLower());
    }
}
