// https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-8.0
namespace Finance;

public static class Constants
{

    // Using environment variables is a simple way to obfuscate sensitive information -
    // Which works fine in a student project like this. In an actual product
    // we need other methods to keep these things secure. (Apparently there are services for this)

    // Using ?? to have a default value, if none has been set in the dev environment. 
    static string Host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    static int Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT") ?? "5432");
    static string Database = Environment.GetEnvironmentVariable("DB_NAME") ?? "financeapp";
    static string Username = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
    static string Password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

    public static string connectionString =
    $"""
    Host={Constants.Host};Port={Constants.Port};Database={Constants.Database};Password={Constants.Password};
    """;

}