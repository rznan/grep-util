namespace Grep.Core;

public class FileReader
{
    public int CurrentLine { get; private set; }
    private StreamReader sr;

    public FileReader(string path)
    {
        if (!Path.Exists(path))
        {
            throw new FileNotFoundException();
        }
        sr = new StreamReader(File.OpenRead(path));
    }

    public string? NextLine()
    {
        string? line = sr.ReadLine();
        if (line != null)
        {
            CurrentLine++;
        }
        return line;
    }
}
