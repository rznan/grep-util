public class FileReaderTests
{
    [Fact]
    public void Constructor_ShouldReturnFileNotFoundException()
    {
        var exception = Assert.Throws<FileNotFoundException>(() =>
            new FileReader("invalid")
        );
    }

    [Fact]
    public void NextLine_ShouldCorrectlyReturnTheNextLine()
    {
        // Arrange
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/text.txt");
        var reader = new FileReader(filePath);

        List<String> expected = new List<string>(new[] { "hello, there", "1", "2", "3" });
        var actual = new List<String>();

        // Act
        string? l;
        while ((l = reader.NextLine()) != null)
        {
            actual.Add(l);
        }

        // Assert
        Assert.Equal(expected, actual);
    }
}
