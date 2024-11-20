using CommunityToolkit.Mvvm.ComponentModel;

using Finance.Data;
using Finance.Models;
using Finance.Utilities;
using CommunityToolkit.Mvvm.Input;
using Finance.Views;


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
        if (!Email.IsValidEmail())
        {
            await Shell.Current.DisplayAlert("Email", "Please enter a valid email", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }


        if (!Password.Equals(RePassword))
        {

            await Shell.Current.DisplayAlert("Password", "Passwords much match", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }


        if (!Password.IsValidPassword())
        {
            await Shell.Current.DisplayAlert("Password", "Passwords must be longer than 8 characters", "OK");
            Password = string.Empty;
            RePassword = string.Empty;
            return;
        }

        // Replace UserManager with a Repository that we can inject through the constructor.
        if (!UserManager.AddUser(Email, Name, Password).Result)
        {
            await Shell.Current.DisplayAlert("Failure", $"Oops - User {Name} has not been created", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Success", $"User {Name} has been created", "OK");

        Email = string.Empty;
        Name = string.Empty;
        Password = string.Empty;
        RePassword = string.Empty;

        try
        {
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Navigation Error", ex.Message, "OK");
        }

    }
}
