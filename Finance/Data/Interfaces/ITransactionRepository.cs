using Finance.Models;

namespace Finance.Data.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task<bool> AddTransactionAsync();
    Task<bool> RemoveTransactionAsync(Guid guid);
}