using CommandLine;
using Grep.Util;
using Grep.Core;

public class Program
{
    private static void Main(string[] args)
    {
        if (!ValidateArgs(args))
        {
            Environment.Exit(HandleError("USAGE: grep <parameters> filename searchTerm", 4));
            return;
        }

        String filename = args[^2];
        String search = args[^1];

        try
        {
            Parser.Default.ParseArguments<CliOpts>(args[0..^2])
                .WithParsed<CliOpts>(opts => Run(opts, filename, search));
        }
        catch (Exception)
        {
            Environment.Exit(HandleError("Something went wrong", 1));
            return;
        }
    }


    private static void Run(CliOpts opts, string filename, string search)
    {
        Search searchEngine;
        try
        {
            searchEngine = new Search(
                new FileReader(filename),
                ParseSearchOptions(opts)
            );
        }
        catch (FileNotFoundException)
        {
            Environment.Exit(HandleError($"Could not find file: {filename}", 2));
            return;
        }
        catch (NotImplementedException e)
        {
            Environment.Exit(HandleError(e.Message, 3));
            return;
        }

        string? result = searchEngine.Execute(search);

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


    private static bool ValidateArgs(string[] args)
    {
        return (args.Length >= 2 && (!args[^1].StartsWith("-") && !args[^2].StartsWith("-")));
    }
}
