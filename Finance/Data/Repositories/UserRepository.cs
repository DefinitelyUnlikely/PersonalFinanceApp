using Finance.Data.Database;
using Finance.Data.Interfaces;
using Finance.Models;
using Npgsql;

namespace Finance.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly IFinanceDatabase database;

    private User? currentUser;
    public User? CurrentUser { get => currentUser; }

    public Dictionary<string, User> userCache = [];

    public UserRepository(IFinanceDatabase database)
    {
        this.database = database;
    }


    public void SetUser(User? user)
    {
        currentUser = user;
    }

    public void ResetUser()
    {
        currentUser = null;
    }

    public async Task<bool> AddUserAsync(string email, string name, string password)
    {

        User addUser = new(email, name, password);


        string sql = @"INSERT INTO users (email, name, salt, password) VALUES (@email, @name, @salt, @password);";
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@email", addUser.Email);
        command.Parameters.AddWithValue("@name", addUser.Name);
        command.Parameters.AddWithValue("@salt", addUser.Salt);
        command.Parameters.AddWithValue("@password", addUser.PasswordHash);

        try
        {
            if (await command.ExecuteNonQueryAsync() == -1)
            {
                return false;
            }

            userCache.Add(addUser.Name, addUser);

            return true;
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Internal User Error", e.Message, "OK");
        }

        return false;

    }

    public async Task<User?> GetUserAsync(string name)
    {

        if (userCache.TryGetValue(name, out User? value))
        {
            return value;
        }

        string sql = @"SELECT * FROM users WHERE name = @name;";
        using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", name);

        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            reader.Read();
            User getUser = new(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            userCache.Add(getUser.Name, getUser);
            return getUser;
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Internal User Error", e.Message, "OK");
        }

        return null;

    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"DELETE FROM users WHERE id = @id;";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);
        try
        {
            return await command.ExecuteNonQueryAsync() != -1;
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Internal User Error", e.Message, "OK");
        }

        return false;
    }

    public async Task<bool> UserExistsAsync(string name)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
        string sql = @"SELECT * FROM users WHERE name = @name;";
        await using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", name);
        return await command.ExecuteNonQueryAsync() != -1;
    }
}