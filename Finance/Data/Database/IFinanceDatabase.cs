using System.Data.Common;
using Finance;

namespace Finance.Data.Database;

public interface IFinanceDatabase
{
    Task<DbConnection> GetConnectionAsync();
    Task InitializeDatabase();
}