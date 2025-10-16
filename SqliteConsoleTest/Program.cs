using Microsoft.Data.Sqlite;

using (var connection = new SqliteConnection("Data Source=:memory:"))
{
    connection.Open();

    var command = new SqliteCommand(
        "SELECT 1;", connection);
    long result = (long)command.ExecuteScalar();
    Console.WriteLine($"Command output: {result}");
}
