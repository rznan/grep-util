namespace Grep.Core.SearchEngine;

public class InvertSearch : BaseSearchEngine
{
    private ISearchEngine baseEngine;

    public InvertSearch(ISearchEngine baseEngine) => this.baseEngine = baseEngine;

    public override bool LineMatch(string line, string target)
    {
        return !baseEngine.LineMatch(line, target);
    }
}
