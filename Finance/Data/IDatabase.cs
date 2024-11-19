using System;
using Npgsql;

namespace Finance.Data;

public interface IDatabase
{
    public Task<NpgsqlConnection> GetConnection();
    public Task InitializeDatabase();
}