using Finance.Models;

namespace Finance.Managers;


// I won't need the list nor will AddUser and RemoveUser look like this/be needed
// but what I will need is away to keep track of the current user and log them in and out. 
// part of that functionality will be in the ViewModels, but they could call on this manager. 
public class UserManager
{
    private static Dictionary<string, User> Users { get; } = [];

    public static User? CurrentUser { get; set; }

    public static bool AddUser(User user)
    {
        try
        {
            Users.Add(user.Name.ToLower(), user);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public static bool RemoveUser(User user)
    {
        return Users.Remove(user.Name.ToLower());
    }

    public static bool UserExists(string name)
    {
        return Users.ContainsKey(name.ToLower());
    }

    public static void SetUser(string name)
    {
        CurrentUser = Users[name.ToLower()];
    }

    public static User GetUser(string name)
    {
        return Users[name.ToLower()];
    }

}