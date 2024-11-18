using Finance.Models;
using Finance.Data;
using Finance.Utilities;
using Npgsql;


namespace Finance.Managers;

public class UserManager
{

    public static User? CurrentUser { get; set; }

    public static async Task<bool> AddUser(string email, string name, string password)
    {

        using var connection = await FinanceDatabase.GetConnection();

        try
        {

            (string salt, string passwordHash) = password.SaltAndHash();

            string sql = @"
            INSERT INTO users (email, name, salt, password)
            VALUES (@email, @name, @salt, @password)
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("email", email);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("salt", salt);
            command.Parameters.AddWithValue("password", passwordHash);

            await command.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public static async Task<bool> RemoveUser(int id)
    {
        using var connection = FinanceDatabase.GetConnection().Result;
        using var sqlTransaction = connection.BeginTransaction();

        try
        {
            string sql = @"
            DELETE FROM users WHERE id = @id;
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("id", id);


            await command.ExecuteNonQueryAsync();
            sqlTransaction.Commit();

            return true;
        }
        catch (Exception e)
        {
            sqlTransaction.Rollback();
            Console.WriteLine(e);
            return false;
        }
    }

    public static bool UserExists(string name)
    {
        using var connection = FinanceDatabase.GetConnection().Result;

        string sql = @"SELECT * FROM users WHERE name = @name";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("name", name);

        var user = command.ExecuteScalarAsync();

        Console.WriteLine(user.Result);
        if (user.Result is not null && user.Result.Equals(name))
        {
            return true;
        }

        return false;
    }

    public static void SetUser(string name)
    {

    }

    public static User GetUser(string name)
    {
        using var connection = FinanceDatabase.GetConnection().Result;

        string sql = @"SELECT * FROM users WHERE name = @name";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("name", name);

        var user = command.ExecuteScalarAsync();


        return new User("placeholder", "placeholder", "placeholder");
    }

}