using Npgsql;
using Finance;

namespace Finance.Data;

public static class FinanceDatabase
{
    private static NpgsqlConnection? connection;

    public static NpgsqlConnection Connection
    {
        get
        {
            // if null, create new connection. If not null, return the connection.
            return connection ??= new NpgsqlConnection(Constants.connectionString);
        }
    }
}

