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
    string email = string.Empty;

    [ObservableProperty]
    string name = string.Empty;

    [ObservableProperty]
    string password = string.Empty;

    [ObservableProperty]
    string rePassword = string.Empty;


    [RelayCommand]
    public async Task CreateAccount()
    {
        if (Email is "" || Name is "" || Password is "" || RePassword is "")
        {
            await Shell.Current.DisplayAlert("Missing fields", "All fields are required", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }

        // In reality, you'd probably also send some kind of verification email
        // to check if the email actually exists. We won't.
        if (!ValidationUtilities.IsValidEmail(Email))
        {
            await Shell.Current.DisplayAlert("Email", "Please enter a valid email", "OK");
            return;
        }


        if (!Password.Equals(RePassword))
        {

            await Shell.Current.DisplayAlert("Password", "Passwords much match", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }


        if (!ValidationUtilities.IsValidPassword(Password))
        {
            await Shell.Current.DisplayAlert("Password", "Passwords must be longer than 8 characters", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }


        // Hopefully npgsql gives returns if inserts don't work, so once
        // this is replaced by the SQL variant - we can try to add the user
        // and if email or name isn't unique, we catch that.
        UserManager.AddUser(new User(Email, Name, Password));
        await Shell.Current.DisplayAlert("Success", $"User {Name} has been created", "OK");

        Email = string.Empty;
        Name = string.Empty;
        Password = string.Empty;
        RePassword = string.Empty;

    }
}
