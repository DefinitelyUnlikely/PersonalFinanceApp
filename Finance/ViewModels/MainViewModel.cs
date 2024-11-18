using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Views;
using Finance.Managers;
using Finance.Utilities;
using System.Reflection;

namespace Finance.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    public string username = string.Empty;

    [ObservableProperty]
    public string password = string.Empty;

    [RelayCommand]
    async Task Forgot(string url)
    {
        try
        {
            await Launcher.OpenAsync(url);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    async Task TryLogin()
    {
        if (Username is "" || Password is "")
        {
            await Shell.Current.DisplayAlert("Login Error", "Please enter both username and password", "OK");
            Password = string.Empty;
            return;
        }

        // The dict and method in UserManager will be replaced by the database
        if (!UserManager.UserExists(Username))
        {
            await Shell.Current.DisplayAlert("Login Error", "That username does not exist", "OK");
            Password = string.Empty;
            return;
        }

        if (!Username.VerifyPassword(Password))
        {
            await Shell.Current.DisplayAlert("Login Error", "Wrong password.", "OK");
            Password = string.Empty;
            return;
        }

        // TODO: Once the DB is set up, move to the transaction page and show only the 
        // transactions that account made. 
        UserManager.SetUser(Username);
        Console.WriteLine($"{MethodBase.GetCurrentMethod()!.DeclaringType!.Name} - Username is {Username}");

        try
        {
            await Shell.Current.GoToAsync(nameof(TransactionView));
            Username = string.Empty;
            Password = string.Empty;
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Nav error", e.Message, "OK");
        }

    }
}
