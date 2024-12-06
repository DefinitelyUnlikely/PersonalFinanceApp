using Npgsql;
using Finance.Data.Interfaces;
using Finance.Data.Database;
using Finance.Models;
using System.Data.Common;
using CommunityToolkit.Maui.Converters;

namespace Finance.Data.Repositories;

// TODO: Add proper try catch/error handling.
public class TransactionRepository : ITransactionRepository
{
    private readonly IFinanceDatabase database;

    public TransactionRepository(IFinanceDatabase database)
    {
        this.database = database;
    }

    public async Task<bool> AddTransactionAsync(Transaction transaction)
    {

        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"INSERT INTO transactions VALUES (@id, @userId, @name, @amount, @created, @date)";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", transaction.Id);
            command.Parameters.AddWithValue("@userId", transaction.AccountId);
            command.Parameters.AddWithValue("@name", transaction.Name);
            command.Parameters.AddWithValue("@amount", transaction.Amount);
            command.Parameters.AddWithValue("@created", transaction.Created);
            command.Parameters.AddWithValue("@date", transaction.Date);

            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            throw new Exception("Transaction Error: " + e.Message);
        }
    }

    public async Task<bool> RemoveTransactionAsync(Guid guid)
    {
        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"DELETE FROM transactions WHERE id = @guid;";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@guid", guid);

            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            throw new Exception("Transaction Error: " + e.Message);
        }


    }


    public async Task<List<Transaction>> GetUserTransactionsAsync(int userId)
    {
        try
        {
            List<Transaction> transactions = [];

            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"
            SELECT transactions.name, transactions.amount, transactions.date, accounts.display_name FROM transactions 
            INNER JOIN accounts ON transactions.account_id = accounts.id WHERE user_id = @userId";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@userId", userId);
            await using var reader = command.ExecuteReader();

            while (reader.Read())
            {

            }

            return transactions;
        }
        catch (Exception e)
        {
            throw new Exception("Transaction Error: " + e.Message);
        }

    }

    // general usecase function - execute any SQL query that returns transactions.
    public async Task<List<Transaction>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Transaction>?>> operation)
    {
        try
        {
            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            return await operation(connection);
        }
        catch (Exception e)
        {
            throw new Exception("Transaction Error: " + e.Message);
        }
    }

    public async Task<List<Transaction>> GetAccountTransactionsAsync(Guid id)
    {
        try
        {
            List<Transaction> transactions = [];

            await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
            string sql = @"SELECT * FROM transactions WHERE account_id = @id";
            await using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);

            await using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                while (reader.Read())
                {
                    Guid transactionId = reader.GetGuid(0);
                    Guid accountId = reader.GetGuid(1);
                    string name = reader.GetString(2);
                    double amount = reader.GetDouble(3);
                    DateTime date = reader.GetDateTime(4);
                    DateTime created = reader.GetDateTime(5);
                    transactions.Add(new Transaction(transactionId, accountId, name, amount, date, created));
                }
            }

            return transactions;
        }
        catch (Exception e)
        {
            throw new Exception("Could not get transactions for account: " + e.Message);
        }
    }
}