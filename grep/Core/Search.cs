using Grep.Core.SearchEngine;
using Grep.Util;

namespace Grep.Core;

public class Search
{
    private ISearchEngine engine;
    private FileReader fr;


    public Search(FileReader fileReader, SearchOpts[] options)
    {
        fr = fileReader;
        engine = SelectSearchEngine(options);
    }


    private ISearchEngine SelectSearchEngine(SearchOpts[] options)
    {
        // Select Base Level Search
        ISearchEngine tmp;
        if (options.Contains(SearchOpts.USE_REGEX))
        {
            throw new NotImplementedException("Regex Support is still in development");
        }
        else
        {
            tmp = new BasicSearch();
        }

        // Select Behavior Modifications
        if (options.Contains(SearchOpts.CASE_INSENSITIVE))
        {
            tmp = new CaseInsensitiveSearch();
        }

        // should be the last
        if (options.Contains(SearchOpts.INVERT_MATCH))
        {
            throw new NotImplementedException("Invert Match is still in development");
        }

        return tmp;
    }


    public String? Execute(string target)
    {
        var response = engine.Search(fr, target);
        if (string.IsNullOrEmpty(response))
        {
            return null;
        }
        return response;

    }
}

