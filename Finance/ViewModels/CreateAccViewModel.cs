using CommunityToolkit.Mvvm.ComponentModel;

using Finance.Data;
using Finance.Models;
using Finance.Managers;
using Finance.Utilities;
using CommunityToolkit.Mvvm.Input;


namespace Finance.ViewModels;

public partial class CreateAccViewModel : ObservableObject
{

    [ObservableProperty]
    string? email;

    [ObservableProperty]
    string? name;

    [ObservableProperty]
    string? password;

    [ObservableProperty]
    string? rePassword;


    [RelayCommand]
    public void CreateAccount()
    {
        // null check
        // Add functionallity telling you that a field is missing (and is required)?
        if (Email is null || Name is null || Password is null || RePassword is null) return;

        if (!Password.Equals(RePassword))
        {

            // reset all fields
            Email = null;
            Name = null;
            Password = null;
            RePassword = null;
            return;
        }

        // In reality, you'd probably also send some kind of verification email
        // to check if the email actually exists. We won't.
        if (!ValidationUtilities.IsValidEmail(Email))
        {
            Email = "ENTER A VALID EMAIL!";
            return;
        }

        string hashedPassword = "";

        // Hopefully npgsql gives returns if inserts don't work, so once
        // this is replaced by the SQL variant - we can try to add the user
        // and if email or name isn't unique, we catch that.
        UserManager.AddUser(new User(Email, Name, hashedPassword));

        Email = null;
        Name = null;
        Password = null;
        RePassword = null;

    }
}
