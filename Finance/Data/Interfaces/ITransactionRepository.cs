using System.Data.Common;
using Finance.Models;

namespace Finance.Data.Interfaces;

// As Interfaces should make things more modular, I optet for a general case "ExecuteOperation" method
// to be able to inject the interface and use it for my very specific needs - but still keep the interface 
// actually being modular/more general. 
public interface ITransactionRepository
{
    Task<List<Transaction>> GetUserTransactionsAsync(int id);
    Task<bool> AddTransactionAsync(Transaction transaction);
    Task<bool> RemoveTransactionAsync(Guid guid);
    Task<List<Transaction>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Transaction>?>> operation);
}