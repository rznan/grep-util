public class SearchEngineTests
{
    private string test01Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text.txt");
    private string test02Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text02.txt");
    private string test03Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text03.txt");

    [Fact]
    public void Execute_BasicSearchShouldReturnNullWhenSearchingForAnUnpresentText()
    {
        // arrange
        var reader = new FileReader(test01Path);
        var options = new SearchOpts[] { };
        var searchController = new Search(reader, options);


        // act
        var response = searchController.Execute("x");

        // assert
        Assert.True(response is null);
    }

    [Theory]
    [InlineData("hello", "hello, there\r\n")]
    [InlineData("2", "2\r\n")]
    public void Execute_ShouldDoABasicSearchAndReturnTheCorrectLine(string searchTerm, string? expectedLines)
    {
        // arrange & act
        var options = new SearchOpts[] { };
        var response = ExecuteSearch(options, test01Path, searchTerm);

        // assert
        Assert.Equal($"{test01Path}: {expectedLines}", response);
    }


    [Theory]
    [InlineData("A", "1a\n2a\n3ab\n4ba\nSalmon\n")]
    [InlineData("b", "3ab\n4ba\n5b\n6b\nBubble\n")]
    public void Execute_ShouldDoACaseInsensitiveSearchAndReturnTheCorrectMatches(string searchTerm, string? expectedLines)
    {
        // arrange & act
        var options = new SearchOpts[] { SearchOpts.CASE_INSENSITIVE };
        var response = ExecuteSearch(options, test02Path, searchTerm);

        // assert
        if (response != null && expectedLines != null)
        {
            var expectedItens = AddPathToExpectedLines(expectedLines, test02Path);
            var actualItens = response.Split("\r\n");
            Assert.Equal(actualItens, expectedItens);
            return;
        }

        Assert.Equal(expectedLines, response);
    }

    [Theory]
    [InlineData("hello", "1\n2\n3\n")]
    public void Execute_ShouldDoAInvertedBasicSearchAndReturnTheCorrectLines(string searchTerm, string? expectedLines)
    {
        // arrange & act
        var options = new SearchOpts[] { SearchOpts.INVERT_MATCH };
        var response = ExecuteSearch(options, test01Path, searchTerm);

        // assert
        if (response != null && expectedLines != null)
        {
            var expectedItens = AddPathToExpectedLines(expectedLines, test01Path);
            var actualItens = response.Split("\r\n");
            Assert.Equal(actualItens, expectedItens);
            return;
        }

        Assert.Equal(expectedLines, response);
    }

    [Theory]
    [InlineData(@"\d", "hello, 1\n, 2\n")]
    public void Execute_ShouldDoARegexSearchAndReturnTheCorrectLines(string searchTerm, string? expectedLines)
    {
        var options = new SearchOpts[] { SearchOpts.USE_REGEX };
        var response = ExecuteSearch(options, test03Path, searchTerm);

        if (response != null && expectedLines != null)
        {
            var expectedItens = AddPathToExpectedLines(expectedLines, test03Path);
            var actualItens = response.Split("\r\n");
            Assert.Equal(actualItens, expectedItens);
            return;
        }

        Assert.Equal(expectedLines, response);
    }

    private string? ExecuteSearch(SearchOpts[] options, string filePath, string searchTerm)
    {
        // arrange
        var reader = new FileReader(filePath);
        var searchController = new Search(reader, options);

        // act
        var response = searchController.Execute(searchTerm);

        return response;
    }


    private string[] AddPathToExpectedLines(string expectedLine, string path)
    {

        List<string> lines = [];
        foreach (string i in expectedLine.Split("\n"))
        {
            if (!string.IsNullOrEmpty(i))
                lines.Add($"{path}: {i}");
            else
                lines.Add(i);
        }
        return lines.ToArray();
    }
}
