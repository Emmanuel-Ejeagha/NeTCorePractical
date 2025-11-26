using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit.Abstractions;

namespace ManningBooksApi.Tests;

public class LogSqlInterceptor : DbCommandInterceptor
{
    public readonly ITestOutputHelper _testOutput;

    public LogSqlInterceptor(ITestOutputHelper testOutput)
        => _testOutput = testOutput;
    
    public override ValueTask<InterceptionResult<DbDataReader>> 
        ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken)
    {
        _testOutput.WriteLine(command.CommandText.Trim());
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
}
