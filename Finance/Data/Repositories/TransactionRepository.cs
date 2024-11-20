using Finance.Data.Interfaces;
using Finance.Models;

namespace Finance.Data.Repositories;

public class TransactionRepository : ITransactionRepository
{
    public Task<bool> AddTransactionAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Transaction>> GetTransactionsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveTransactionAsync(Guid guid)
    {
        throw new NotImplementedException();
    }
}