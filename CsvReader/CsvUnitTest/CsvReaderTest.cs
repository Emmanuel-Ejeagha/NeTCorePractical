using CsvParser;



namespace CSVUnitTest;

public class CsvReaderTest : IDisposable
{
    private StreamReader streamReader;
    private CsvReader csvReader;

    public CsvReaderTest()
    {
        streamReader = new StreamReader(new FileStream("Marvel.csv", FileMode.Open));
        csvReader = new CsvReader(streamReader);
    }

    public void Dispose()
    {
        streamReader.Dispose();
    }

    [Fact]
    public void VerifyNumberOfLines()
    {
        Assert.Equal(7, csvReader.Lines.Count());
    }
}
