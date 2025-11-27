using System;
using Microsoft.EntityFrameworkCore;

namespace MoneyTransfer.Tests;

public class BankRepositoryTests
{
    private readonly BankContext _bankContext;
    private readonly BankRepository _bankRepo;

    public BankRepositoryTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=bank;User Id=sa;Password=SQLserver67@@;TrustServerCertificate=true;Trusted_Connection=Yes;");
        var options = optionsBuilder.Options;
        _bankContext = new(options);
        _bankContext.Database.EnsureCreated();
        _bankRepo = new(_bankContext);
    }
}
