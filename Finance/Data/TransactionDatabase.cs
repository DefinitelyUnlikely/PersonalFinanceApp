using SQLite;
using System.Diagnostics;
using Finance.Models;


namespace Finance.Data;

public class TransactionDatabase
{
    SQLiteAsyncConnection Database;

    public TransactionDatabase()
    {
        Console.WriteLine(Constants.DatabasePath);
        Debug.WriteLine(Constants.DatabasePath);
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        Database.CreateTableAsync<Transaction>().Wait();
    }

    public async Task<List<Transaction>> GetItemsAsync()
    {
        return await Database.Table<Transaction>().ToListAsync();
    }

    public async Task<Transaction> GetItemAsync(int id)
    {
        return await Database.Table<Transaction>()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Transaction item)
    {
        if (item.Id == 0)
        {
            return await Database.InsertAsync(item);
        }
        return await Database.UpdateAsync(item);
    }

    public async Task<int> DeleteItemAsync(Transaction item)
    {
        return await Database.DeleteAsync(item);
    }

    public async Task<double> GetBalanceAsync()
    {
        List<Transaction> transactions = await GetItemsAsync();
        return transactions.Sum(x => x.Amount);
    }
}
