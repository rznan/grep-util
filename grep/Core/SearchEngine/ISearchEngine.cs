namespace Grep.Core.SearchEngine;

public interface ISearchEngine
{
    bool LineMatch(string line, string target);
    string Search(FileReader fileReader, string target);
}
