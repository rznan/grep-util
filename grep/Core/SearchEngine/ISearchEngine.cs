namespace Grep.Core.SearchEngine;

internal interface ISearchEngine
{
    bool LineMatch(string line, string target);
    string Search(FileReader fileReader, string target);
}
