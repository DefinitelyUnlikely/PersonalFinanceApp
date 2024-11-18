using Npgsql;
using Finance;
using System.Transactions;

namespace Finance.Data;

public class FinanceDatabase
{
    private static NpgsqlConnection? connection;


    public static async Task<NpgsqlConnection> GetConnection()
    {
        if (connection is not null)
        {
            await connection.OpenAsync();
            return connection;
        }

        try
        {
            connection = new NpgsqlConnection(Constants.connectionString);
            Console.WriteLine(Constants.connectionString);
            await connection.OpenAsync();
            return connection;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public static async Task CreateTablesIfNotExists()
    {
        string createUsersTable = @"
        CREATE TABLE IF NOT EXISTS users (
        id SERIAL PRIMARY KEY,
        email text UNIQUE NOT NULL,
        name text UNIQUE NOT NULL,
        salt text NOT NULL,
        password text NOT NULL
        )";

        string createTransactionsTable = @"
        CREATE TABLE IF NOT EXISTS transactions (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        userId INTEGER REFERENCES users(id),
        name text NOT NULL,
        amount decimal NOT NULL,
        created DATE NOT NULL,
        date DATE NOT NULL
        )";

        await using var conn = await GetConnection();
        await using var sqlTransaction = await conn.BeginTransactionAsync();

        try
        {
            await using var command1 = new NpgsqlCommand(createUsersTable, conn, sqlTransaction);
            await command1.ExecuteNonQueryAsync();

            await using var command2 = new NpgsqlCommand(createTransactionsTable, conn, sqlTransaction);
            await command2.ExecuteNonQueryAsync();

            sqlTransaction.Commit();
        }
        catch (Exception e)
        {
            sqlTransaction.Rollback();
            await Shell.Current.DisplayAlert("Database Error", "Something went wrong with the creation of the tables: " + e.Message, "OK");
        }

    }
}

