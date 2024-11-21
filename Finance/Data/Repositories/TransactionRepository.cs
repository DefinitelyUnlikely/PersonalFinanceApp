using Npgsql;
using Finance.Data.Interfaces;
using Finance.Data.Database;
using Finance.Models;
using System.Data;

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

        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"INSERT INTO transactions VALUES (@id, @userId, @name, @amount, @created, @date)";
        await using var command = new NpgsqlCommand(sql, connection);

        Console.WriteLine($"{transaction.Id} {transaction.UserId} {transaction.Name} {transaction.Amount} {transaction.Date}");
        command.Parameters.AddWithValue("@id", transaction.Id);
        command.Parameters.AddWithValue("@userId", transaction.UserId);
        command.Parameters.AddWithValue("@name", transaction.Name);
        command.Parameters.AddWithValue("@amount", transaction.Amount);
        command.Parameters.AddWithValue("@created", transaction.Created);
        command.Parameters.AddWithValue("@date", transaction.Date);

        return await command.ExecuteNonQueryAsync() != -1;
    }

    public async Task<bool> RemoveTransactionAsync(Guid guid)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"DELETE FROM transactions WHERE id = @guid;";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@guid", guid);

        return await command.ExecuteNonQueryAsync() != -1;

    }


    public async Task<List<Transaction>> GetUserTransactionsAsync(int userId)
    {

        List<Transaction> transactions = [];

        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"SELECT * FROM transactions WHERE userId = @userId;";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@userId", userId);
        await using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            Guid guid = reader.GetGuid(0);
            int id = reader.GetInt32(1);
            string name = reader.GetString(2);
            double amount = reader.GetDouble(3);
            DateTime date = reader.GetDateTime(4);
            DateTime created = reader.GetDateTime(5);
            transactions.Add(new Transaction(guid, id, name, amount, date, created));
        }

        return transactions;

    }

}