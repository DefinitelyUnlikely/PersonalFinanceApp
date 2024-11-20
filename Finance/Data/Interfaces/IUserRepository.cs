using Finance.Models;

namespace Finance.Data.Interfaces;


public interface IUserRepository
{
    User? CurrentUser { get; }
    Task<bool> AddUserAsync(string email, string name, string password);
    Task<bool> RemoveUserAsync(int id);
    Task<bool> UserExistsAsync(string name);
    Task<User?> GetUserAsync(string name);
    void SetCurrentUser(User user);
}