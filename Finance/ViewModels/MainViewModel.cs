using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Managers;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    public string name = string.Empty;

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
        if (Name is "" || Password is "")
        {
            await Shell.Current.DisplayAlert("Login Error", "Please enter both username and password", "OK");
            Password = string.Empty;
            return;
        }

        // The dict and method in UserManager will be replaced by the database
        if (!UserManager.UserExists(Name))
        {
            await Shell.Current.DisplayAlert("Login Error", "That username does not exist", "OK");
            return;
        }

        if (!Name.VerifyPassword(Password))
        {
            await Shell.Current.DisplayAlert("Login Error", "Wrong password.", "OK");
        }

        UserManager.SetUser(Name);
    }
}
