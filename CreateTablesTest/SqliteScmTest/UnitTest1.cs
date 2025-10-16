namespace SqliteScmTest;

public class UnitTest1 : IClassFixture<SampleScmDataFixture>
{
    private SampleScmDataFixture fixture;

    public UnitTest1(SampleScmDataFixture fixture)
    {
        this.fixture = fixture;
    }
    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }
}
