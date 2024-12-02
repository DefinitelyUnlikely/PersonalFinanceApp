using System;

namespace Finance.Models;

public class Account
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public string Name { get; set; }

    public Account(int userId, string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
    }
}
