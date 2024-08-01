public class SearchEngineTests
{
    private string test01Path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text.txt");

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
        // arrange
        var reader = new FileReader(test01Path);
        var options = new SearchOpts[] { };
        var searchController = new Search(reader, options);

        // act
        var response = searchController.Execute(searchTerm);

        // assert
        Assert.Equal(expectedLine, response);
    }

}
