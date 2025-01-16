using Npgsql;
using System.Data.Common;

namespace Finance.Data.Database;

public class PostgresDatabase : IFinanceDatabase
{

    private readonly string connectionString;

    public PostgresDatabase()
    {
        connectionString = Constants.connectionString;
    }

    // Should most of this be in my constructor perhaps? 
    // And we only return the connection?

    public async Task<DbConnection> GetConnectionAsync()
    {
        var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }


    public async Task InitializeDatabase()
    {
        string createUsersTable = @"
        CREATE TABLE IF NOT EXISTS users (
        id SERIAL PRIMARY KEY,
        email text UNIQUE NOT NULL,
        display_name text NOT NULL,
        user_name text GENERATED ALWAYS AS (UPPER(display_name)) STORED, 
        salt text NOT NULL,
        password text NOT NULL,
        UNIQUE(user_name)
        )";

        string createAccountsTable = @"
        CREATE TABLE IF NOT EXISTS accounts (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        user_id INTEGER REFERENCES users(id),
        display_name TEXT NOT NULL,
        account_name text GENERATED ALWAYS AS (UPPER(display_name)) STORED,
        UNIQUE(user_id, account_name)
        )";

        string createTransactionsTable = @"
        CREATE TABLE IF NOT EXISTS transactions (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        account_id UUID REFERENCES accounts(id),
        name text NOT NULL,
        amount decimal NOT NULL,
        date DATE NOT NULL,
        created DATE NOT NULL
        )";

        await using var conn = (NpgsqlConnection)await GetConnectionAsync();
        await using var sqlTransaction = await conn.BeginTransactionAsync();

        try
        {
            await using var command1 = new NpgsqlCommand(createUsersTable, conn, sqlTransaction);
            await command1.ExecuteNonQueryAsync();

            await using var command2 = new NpgsqlCommand(createAccountsTable, conn, sqlTransaction);
            await command2.ExecuteNonQueryAsync();

            await using var command3 = new NpgsqlCommand(createTransactionsTable, conn, sqlTransaction);
            await command3.ExecuteNonQueryAsync();

            await sqlTransaction.CommitAsync();
        }
        catch (Exception e)
        {
            await sqlTransaction.RollbackAsync();
            throw new Exception("Database Error: Something went wrong with the creation of the tables: " + e.Message);
        }

    }

}

