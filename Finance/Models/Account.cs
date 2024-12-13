using System;
using System.ComponentModel;

namespace Finance.Models;

public class Account
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public string DisplayName { get; set; }
    public string Name { get; set; }


    // new account
    public Account(int userId, string displayName)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        DisplayName = displayName;
        Name = DisplayName.ToUpper();
    }

    public Account(Guid id, int userId, string displayName, string name)
    {
        Id = id;
        UserId = userId;
        DisplayName = displayName;
        Name = name;
    }
}
