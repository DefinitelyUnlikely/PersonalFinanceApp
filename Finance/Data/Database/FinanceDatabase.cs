using Npgsql;
using System.Data.Common;

namespace Finance.Data.Database;

public class FinanceDatabase : IFinanceDatabase
{

    private readonly string connectionString;

    public FinanceDatabase()
    {
        connectionString = Constants.connectionString;
    }

    public async Task<DbConnection> GetConnectionAsync()
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }

    // What is a good name for this function? 
    public async Task ExecuteDbQuery()
    {
        // A recommended to me approach to not have to create a new database object 
        // everytime I want a connection is to have a method that takes a func as it's argument
    }

    public async Task InitializeDatabase()
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

        await using var conn = (NpgsqlConnection)await GetConnectionAsync();
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

