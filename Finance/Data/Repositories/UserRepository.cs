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

    public UserRepository(IFinanceDatabase database)
    {
        this.database = database;
    }

    public async Task<bool> AddUserAsync(string email, string name, string password)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
    }

    public async Task<User?> GetUserAsync(string name)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();

    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
    }

    public async void SetCurrentUser(User user)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
    }

    public async Task<bool> UserExistsAsync(string name)
    {
        await using var connection = (NpgsqlConnection)await database.GetConnectionAsync();
    }
}