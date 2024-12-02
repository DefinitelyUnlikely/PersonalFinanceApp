using System;
using System.Data.Common;
using Finance.Models;

namespace Finance.Data.Interfaces;

public interface IAccountRepository
{
    Task<bool> AddAccountAsync(Account account);
    Task<Account> GetAccountAsync(string name);
    Task<bool> UpdateAccountAsync(Guid guid, Dictionary<string, string> columnsValues);
    Task<bool> DeleteAccountASync(Guid guid);
    Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation);
}
