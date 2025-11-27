using System;
using Microsoft.EntityFrameworkCore;

namespace MoneyTransfer;

public class BankContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public BankContext(DbContextOptions options) : base(options) {}
}
