using SQLite;
using System.Diagnostics;


namespace Finance.Data;

public class TransactionDatabase
{
    SQLiteAsyncConnection Database;

    public TransactionDatabase()
    {
        Console.WriteLine(Constants.DatabasePath);
        Debug.WriteLine(Constants.DatabasePath);
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        Database.CreateTableAsync<Model.Transaction>().Wait();
    }

    public async Task<List<Model.Transaction>> GetItemsAsync()
    {
        return await Database.Table<Model.Transaction>().ToListAsync();
    }

    public async Task<Model.Transaction> GetItemAsync(int id)
    {
        return await Database.Table<Model.Transaction>()
            .Where(i => i.TransactionId == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Model.Transaction item)
    {
        if (item.TransactionId == 0)
        {
            return await Database.InsertAsync(item);
        }
        return await Database.UpdateAsync(item);
    }

    public async Task<int> DeleteItemAsync(Model.Transaction item)
    {
        return await Database.DeleteAsync(item);
    }

    public async Task<double> GetBalanceAsync()
    {
        List<Model.Transaction> transactions = await GetItemsAsync();
        return transactions.Sum(x => x.TransactionAmount);
    }
}
