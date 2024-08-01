using CommandLine;

namespace Grep.Util;

public class CliOpts
{
    [Option('f', "file", Required = true, HelpText = "Path to the input file")]
    public string file { get; set; } = string.Empty;

    [Option('s', "search", Required = true, HelpText = "Text to be searched")]
    public string searchTerm { get; set; } = string.Empty;

    [Option('i', "caseInsensitive", Required = false, HelpText = "Ignore letter cases when searching")]
    public bool caseInsensitive { get; set; }

    [Option('E', "regex", Required = false, HelpText = "Search using regular expressions")]
    public bool regex { get; set; }

    [Option('v', "invert", Required = false, HelpText = "Find lines that don't match the search term")]
    public bool invert { get; set; }
}

