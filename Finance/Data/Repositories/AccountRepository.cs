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
    public Account? selectedAccount { get => selectedAcc; } // Keep the id instead? Easier when updating.

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
        }
        catch (Exception e)
        {
            throw new Exception("Could not add user: " + e.Message);
        }
    }

    public Task<bool> DeleteAccountASync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAccountAsync(Guid guid, Dictionary<string, string> columnsValues)
    {
        throw new NotImplementedException();
    }
}
