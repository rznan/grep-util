public class SearchEngineTests
{
    private string test01Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text.txt");
    private string test02Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text02.txt");

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
    [InlineData("4", null)]
    public void Execute_ShouldDoABasicSearchAndReturnTheCorrectLine(string searchTerm, string? expectedLine)
    {
        // arrange & act
        var options = new SearchOpts[] { };
        var response = ExecuteSearch(options, test01Path, searchTerm, expectedLine);

        // assert
        Assert.Equal(expectedLine, response);
    }


    [Theory]
    [InlineData("A", "1a\n2a\n3ab\n4ba\nSalmon\n")]
    [InlineData("b", "3ab\n4ba\n5b\n6b\nBubble\n")]
    public void Execute_ShouldDoACaseInsensitiveSearchAndReturnTheCorrectMatches(string searchTerm, string? expectedLine)
    {
        // arrange & act
        var options = new SearchOpts[] { SearchOpts.CASE_INSENSITIVE };
        var response = ExecuteSearch(options, test02Path, searchTerm, expectedLine);

        // assert
        if (response != null && expectedLine != null)
        {
            var expectedItens = expectedLine.Split("\n");
            var actualItens = response.Split("\r\n");
            Assert.Equal(actualItens, expectedItens);
            return;
        }

        Assert.Equal(expectedLine, response);
    }


    private string? ExecuteSearch(SearchOpts[] options, string filePath, string searchTerm, string? expectedLine)
    {
        // arrange
        var reader = new FileReader(filePath);
        var searchController = new Search(reader, options);

        // act
        var response = searchController.Execute(searchTerm);

        return response;
    }
}
