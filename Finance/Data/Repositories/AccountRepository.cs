using System;
using System.Data.Common;
using Finance.Data.Database;
using Finance.Data.Interfaces;
using Finance.Models;
using Npgsql;

namespace Finance.Data.Repositories;

public class AccountRepository : IAccountRepository
{

    private readonly IFinanceDatabase database;

    private Account? selectedAcc;
    public Account? SelectedAccount { get => selectedAcc; }

    public Dictionary<string, Account> accountCache = [];

    public AccountRepository(IFinanceDatabase financeDatabase)
    {
        database = financeDatabase;
    }

    public async Task<bool> AddAccountAsync(Account account)
    {
        accountCache.Add(account.Name, account);

        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"INSERT INTO accounts VALUES (@id, @userId, @name)";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", account.Id);
            command.Parameters.AddWithValue("@userId", account.UserId);
            command.Parameters.AddWithValue("@name", account.Name);

            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            throw new Exception("Could not add user: " + e.Message);
        }
    }

    public async Task<bool> DeleteAccountAsync(Guid guid)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"DELETE FROM accounts WHERE id = @id";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", guid);

        return await command.ExecuteNonQueryAsync() != -1;
    }

    public async Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> GetAccountAsync(string name)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"DELETE FROM accounts WHERE id = @id";
        await using var command = new NpgsqlCommand(sql, connection);
    }

    public async Task<bool> UpdateAccountAsync(Guid guid, Dictionary<string, string> columnsValues)
    {
        throw new NotImplementedException();
    }

    public void SetAccount(string name)
    {
        if (accountCache.TryGetValue(name, out Account? value))
        {
            selectedAcc = value;
            return;
        }

        throw new Exception("That account is not available in the cache.");
    }
}
