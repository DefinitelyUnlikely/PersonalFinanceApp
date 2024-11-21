using System.Data.Common;

namespace Finance.Data.Database;

public interface IFinanceDatabase
{
    Task<DbConnection> GetConnectionAsync();
    Task InitializeDatabase();
}