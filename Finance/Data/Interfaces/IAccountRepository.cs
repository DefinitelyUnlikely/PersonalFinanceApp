using System;
using System.Data.Common;
using Finance.Models;

namespace Finance.Data.Interfaces;

public interface IAccountRepository
{
    void SetAccount(string name);
    Task<bool> AddAccountAsync(Account account);
    Task<Account> GetAccountAsync(string name);
    Task<bool> UpdateAccountAsync(Guid guid, Dictionary<string, string> columnsValues);
    Task<bool> DeleteAccountAsync(Guid guid);
    Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation);
}
