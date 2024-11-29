using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Views;
using Finance.Utilities;
using Finance.Data.Interfaces;
using Finance.Models;

namespace Finance.ViewModels;

public partial class MainViewModel : ObservableObject
{

    private readonly IUserRepository userRepo;
    private readonly IPasswordUtilities passwordUtilities;

    [ObservableProperty]
    public string username = string.Empty;

    [ObservableProperty]
    public string password = string.Empty;

    public MainViewModel(IUserRepository ur, IPasswordUtilities pu)
    {
        passwordUtilities = pu;
        userRepo = ur;
    }

    [RelayCommand]
    async static Task Forgot(string url)
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

        if (!await userRepo.UserExistsAsync(Username) || !await passwordUtilities.VerifyPassword(Username, Password))
        {
            await Shell.Current.DisplayAlert("Login Error", "Wrong username or password", "OK");
            Password = string.Empty;
            return;
        }

        try
        {
            User? user = await userRepo.GetUserAsync(Username);
            if (user is not null)
            {
                userRepo.SetUser(user);
            }
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
