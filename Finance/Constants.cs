// https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
namespace Finance;

public static class Constants
{
    public const string DatabaseFilename = "TransactionSQLite.db3";

    public const SQLite.SQLiteOpenFlags Flags =
    SQLite.SQLiteOpenFlags.ReadWrite |
    SQLite.SQLiteOpenFlags.Create |
    SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
