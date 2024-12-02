using System;
using System.Data.Common;
using Finance.Data.Interfaces;
using Finance.Models;

namespace Finance.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    public Dictionary<string, Account> accountCache = [];

    public Task<bool> AddAccountAsync(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAccountASync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>?> ExecuteOperationAsync(Func<DbConnection, Task<List<Account>?>> operation)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAccountAsync(Guid guid, Dictionary<string, string> columnsValues)
    {
        throw new NotImplementedException();
    }
}
