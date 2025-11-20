using System;
using System.Text;
using FakeItEasy;

namespace CustomerBatchImporter.UnitTests;

public class CsvImporterTests
{
    private readonly ICustomerRepository _fakeCustomerRepo;
    private readonly CsvImporter _csvImporter;

    public CsvImporterTests()
    {
        _fakeCustomerRepo = A.Fake<ICustomerRepository>();
        _csvImporter = new(_fakeCustomerRepo);
    }

    private Stream GetStreamFromString(string content) =>
        new MemoryStream(Encoding.UTF8.GetBytes(content));

    [Fact]
    public async Task OneCustomer()
    {
        string email = "some@gmail.com";
        string name = " A Customer";
        string licence = "Basic";
        string csv = string.Join(',', email, name, licence);
    }

    [Fact]
    public async Task ValidCustomerOneLine()
    {
        // Arrange
        string email = "Some@email.com";
        string name = "A Customer";
        string license = "Basic";
        string csv = string.Join(',', email, name, license);
        A.CallTo(() => _fakeCustomerRepo.GetByEmailAsync(email))
            .Returns(default(Customer));

        // Act
        var stream = GetStreamFromString(csv);
        await _csvImporter.ReadAsync(stream);

        // Assert
        A.CallTo(() =>
            _fakeCustomerRepo.GetByEmailAsync(email))
            .MustHaveHappened();
        A.CallTo(() => _fakeCustomerRepo.CreateAsync(
            A<NewCustomerDto>.That.Matches(n =>
                n.Email == email
                && n.Name == name
                && n.License == license)))
        .MustHaveHappened();
    }

    [Fact]
    public async Task InvalidLine()
    {
        // Given
        var stream = GetStreamFromString("not a valid line");

        // When
        await _csvImporter.ReadAsync(stream);

        // Then
        var calls = Fake.GetCalls(_fakeCustomerRepo);
        Assert.Empty(calls);
    }
    [Fact]
    public async Task ThreeLinesOneInvalid()
    {
        // Arrange
        A.CallTo(() => _fakeCustomerRepo.GetByEmailAsync(A<string>.Ignored))
        .Returns(default(Customer));

        // Act
        var stream = GetStreamFromString(
            "a@b.com,customer1,None\ninvalidline\nc@d.com,customer2,None");
        await _csvImporter.ReadAsync(stream);

        // Assert
        A.CallTo(() => _fakeCustomerRepo
            .CreateAsync(A<NewCustomerDto>.Ignored))
            .MustHaveHappenedTwiceExactly();
    }

    [Fact]
    public async Task GetThrows()
    {
        A.CallTo(() => _fakeCustomerRepo.GetByEmailAsync(""))
            .Throws<ArgumentException>();

        var stream = GetStreamFromString(",name,licence");
        await Assert.ThrowsAsync<ArgumentException>(
            () => _csvImporter.ReadAsync(stream));
    }
}
