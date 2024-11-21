using Finance.Models;

namespace Finance.Data.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetUserTransactionsAsync(int id);
    Task<bool> AddTransactionAsync(Transaction transaction);
    Task<bool> RemoveTransactionAsync(Guid guid);
}