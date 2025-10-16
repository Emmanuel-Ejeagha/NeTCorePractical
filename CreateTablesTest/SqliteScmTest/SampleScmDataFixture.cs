using System;
using Microsoft.Data.Sqlite;

namespace SqliteScmTest;

public class SampleScmDataFixture : IDisposable
{
    public SqliteConnection Connection { get; private set; }

    public SampleScmDataFixture()
    {
        Connection = new SqliteConnection("Data Source=:memory:");
        Connection.Open();

        // Create table
        using (var createCommand = new SqliteCommand(
            @"CREATE TABLE PartType(
                Id INTEGER PRIMARY KEY,
                Name VARCHAR(255) NOT NULL
            );", Connection))
        {
            createCommand.ExecuteNonQuery();
        }

        // Insert test data
        using (var insertCommand = new SqliteCommand(
            @"INSERT INTO PartType (Name)
              VALUES ('8289 L-shaped plate');", Connection))
        {
            insertCommand.ExecuteNonQuery();
        }
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}
