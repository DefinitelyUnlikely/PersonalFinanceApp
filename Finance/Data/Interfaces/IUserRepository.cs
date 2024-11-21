using Finance.Models;

namespace Finance.Data.Interfaces;


public interface IUserRepository
{
    User? CurrentUser { get; }
    Task<bool> AddUserAsync(string email, string name, string salt, string password);
    Task<bool> UpdateUserAsync(int id, Dictionary<string, string> columnsValues);
    Task<bool> RemoveUserAsync(int id);
    Task<bool> UserExistsAsync(string name);
    Task<User?> GetUserAsync(string name);
    void SetUser(User user);
    void ResetUser();
}