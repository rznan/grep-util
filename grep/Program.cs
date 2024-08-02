using CommandLine;
using Grep.Util;
using Grep.Core;

public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Parser.Default.ParseArguments<CliOpts>(args)
                .WithParsed<CliOpts>(opts => Run(opts));
        }
        catch (Exception)
        {
            Environment.Exit(HandleError("Something went wrong", 1));
            return;
        }
    }


    private static void Run(CliOpts opts)
    {
        Search searchEngine;
        try
        {
            searchEngine = new Search(
                new FileReader(opts.file),
                ParseSearchOptions(opts)
            );
        }
        catch (FileNotFoundException)
        {
            Environment.Exit(HandleError($"Could not find file: {opts.file}", 2));
            return;
        }
        catch (NotImplementedException e)
        {
            Environment.Exit(HandleError(e.Message, 3));
            return;
        }

        string? result = searchEngine.Execute(opts.searchTerm);

        if (result != null)
        {
            Console.WriteLine(result);
        }

        Environment.Exit(0);
    }


    private static SearchOpts[] ParseSearchOptions(CliOpts opts)
    {
        List<SearchOpts> sOpts = [];

        if (opts.regex) sOpts.Add(SearchOpts.USE_REGEX);
        if (opts.invert) sOpts.Add(SearchOpts.INVERT_MATCH);
        if (opts.caseInsensitive) sOpts.Add(SearchOpts.CASE_INSENSITIVE);

        return sOpts.ToArray();
    }


    private static int HandleError(string message, int errorCode)
    {
        Console.Error.WriteLine(message);
        return errorCode;
    }
}
