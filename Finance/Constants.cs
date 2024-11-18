// https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
namespace Finance;

public static class Constants
{

    // Using ?? to have a default value, if none has been set in the dev environment. 
    static readonly string Host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    static readonly int Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT") ?? "5432");
    static readonly string Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "financeapp";
    static readonly string Username = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
    static readonly string Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password";

    public static string connectionString =
    $"""
    Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};
    """;

}