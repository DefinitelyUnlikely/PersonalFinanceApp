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

    private Account? currentAccount;
    public Account? CurrentAccount { get => currentAccount; }

    public Dictionary<Guid, Account> accountCache = [];

    public AccountRepository(IFinanceDatabase financeDatabase, IUserRepository ur)
    {
        database = financeDatabase;
        userRepo = ur;
    }

    public async Task<bool> AddAccountAsync(Account account)
    {
        accountCache.Add(account.Id, account);

        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"INSERT INTO accounts VALUES (@id, @userId, @name)";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", account.Id);
            command.Parameters.AddWithValue("@userId", account.UserId);
            command.Parameters.AddWithValue("@name", account.DisplayName);

            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            throw new Exception("Could not ADD user: " + e.Message + " AccountRepository.cs\\AddAccountAsync\n");
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
            throw new Exception("Could not DELETE account: " + e.Message + " AccountRepository.cs\\DeleteAccountAsync\n");
        }

    }

    public async Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation)
    {
        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            return await operation(connection);
        }
        catch (Exception e)
        {
            throw new Exception("Account Error: " + e.Message + " AccountRepository.cs\n");
        }
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
            throw new Exception("Could not GET account: " + e.Message + " AccountRepository.cs\n");
        }


    }

    public async Task<List<Account>> GetUserAccountsAsync()
    {

        if (userRepo.CurrentUser is null)
        {
            throw new Exception("User is null, please login before using this method." + " AccountRepository.cs\\GetUserAccountsAsync\n");
        }
        try
        {
            List<Account> accounts = [];

            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();

            string sql = @"SELECT * FROM accounts WHERE user_id = @id";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", userRepo.CurrentUser.Id);
            await using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {

                Account accObj = new(
                    reader.GetGuid(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3)
                    );

                accounts.Add(accObj);
                accountCache.TryAdd(accObj.Id, accObj);
            }

            return accounts;
        }
        catch (Exception e)
        {
            throw new Exception($"Could not load accounts for user: {e.Message}" + " AccountRepository.cs\\GetUserAccountsAsync\n");
        }
    }

    // Only the display name can be updated on an account, so unlike the users
    // update method, this only needs to know which account and which name.
    public async Task<bool> UpdateAccountAsync(Guid guid, string name)
    {

        if (CurrentAccount is null)
        {
            throw new Exception("Account is null, cannot update" + " AccountRepository.cs\\UpdateAccountAsync\n");
        }


        if (userRepo.CurrentUser is null)
        {
            throw new Exception("User is null, cannot update" + " AccountRepository.cs\\UpdateAccountAsync\n");
        }

        if (CurrentAccount.UserId != userRepo.CurrentUser.Id)
        {
            throw new Exception("Current user is not owner of account. May not update." + " AccountRepository.cs\\UpdateAccountAsync\n");
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
            throw new Exception("Could not UPDATE account name: " + e.Message + " AccountRepository.cs\\UpdateAccountAsync\n");
        }

    }

    public void SetAccount(Guid? id)
    {
        if (id is null)
        {
            currentAccount = null;
            return;
        }

        if (accountCache.TryGetValue((Guid)id, out Account? value))
        {
            currentAccount = value;
            return;
        }

        throw new Exception("That account is not available in the cache." + " AccountRepository.cs\\SetAccount \n");
    }
}
