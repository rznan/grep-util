namespace Grep.Core.SearchEngine;

public abstract class BaseSearchEngine : ISearchEngine
{
    public abstract bool LineMatch(string line, string target);

    public virtual string Search(
        FileReader fr,
        string target)
    {
        using (StringWriter sw = new StringWriter())
        {
            for (string? line = fr.NextLine(); line != null; line = fr.NextLine())
            {
                if (LineMatch(line, target))
                {
                    sw.WriteLine(line);
                }
            }
            return sw.ToString();
        }
    }
}
