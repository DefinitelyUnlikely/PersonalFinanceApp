using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Managers;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class PasswordPopupViewModel : ObservableObject
{
    [ObservableProperty]
    string password = string.Empty;

    [ObservableProperty]
    string rePassword = string.Empty;

    [RelayCommand]
    async Task ChangePassword()
    {
        if (!Password.Equals(RePassword))
        {
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must match", "OK");
            return;
        }

        if (!Password.IsValidPassword())
        {
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must be at least 8 characters", "OK");
            return;
        }

        (UserManager.CurrentUser!.Salt, UserManager.CurrentUser!.PasswordHash) = Password.SaltAndHash();
    }
}
