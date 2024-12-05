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
    private readonly IUserRepository userRepo;

    private Account? selectedAcc;
    public Account? SelectedAccount { get => selectedAcc; }

    public Dictionary<string, Account> accountCache = [];

    public AccountRepository(IFinanceDatabase financeDatabase, IUserRepository ur)
    {
        database = financeDatabase;
        userRepo = ur;
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
            throw new Exception("Could not ADD user: " + e.Message);
        }
    }

    public async Task<bool> DeleteAccountAsync(Guid guid)
    {
        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"DELETE FROM accounts WHERE id = @id";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", guid);

            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            throw new Exception("Could not DELETE account: " + e.Message);
        }

    }

    public async Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> GetAccountAsync(string name)
    {
        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"SELECT * FROM accounts WHERE account_name = @account_name";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@account_name", name.ToUpper());

            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.Read())
            {
                throw new ArgumentException("No account with that name.");
            }

            return new Account(reader.GetGuid(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));

        }
        catch (Exception e)
        {
            throw new Exception("Could not GET account: " + e.Message);
        }


    }

    // Only the display name can be updated on an account, so unlike the users
    // update method, this only needs to know which account and which name.
    public async Task<bool> UpdateAccountAsync(Guid guid, string name)
    {

        if (selectedAcc is null)
        {
            throw new Exception($"Account is null, cannot update");
        }


        if (userRepo.CurrentUser is null)
        {
            throw new Exception($"User is null, cannot update");
        }

        if (selectedAcc.UserId != userRepo.CurrentUser.Id)
        {
            throw new Exception($"Current user is not owner of account. May not update.");
        }

        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"UPDATE accounts SET display_name = @name WHERE id = @id";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@id", guid);

            return await command.ExecuteNonQueryAsync() != -1;

        }
        catch (Exception e)
        {
            throw new Exception("Could not UPDATE account name: " + e.Message);
        }

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
