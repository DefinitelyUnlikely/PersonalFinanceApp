using System;
using System.Data.Common;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Database;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class PasswordPopupViewModel : ObservableObject
{
    private readonly IPopupService popupService;
    private readonly IUserRepository userRepo;
    private readonly IFinanceDatabase database;
    private readonly IPasswordUtilities passwordUtilities;

    public PasswordPopupViewModel(IPopupService ps, IUserRepository ur, IFinanceDatabase db, IPasswordUtilities pu)
    {
        database = db;
        userRepo = ur;
        popupService = ps;
        passwordUtilities = pu;
    }

    [ObservableProperty]
    string currentPassword = string.Empty;
    [ObservableProperty]
    string password = string.Empty;

    [ObservableProperty]
    string rePassword = string.Empty;

    [RelayCommand]
    async Task ChangePassword()
    {

        User? user = await userRepo.GetUserAsync(userRepo.CurrentUser!.Name);

        if (user is null)
        {
            await Shell.Current.DisplayAlert("Internal User Error", "CurrentUser is null", "OK");
            return;
        }

        if (CurrentPassword is "" || Password is "" || RePassword is "")
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Entry error", "Please enter all fields", "OK");
            return;
        }

        if (!passwordUtilities.VerifyPassword(userRepo.CurrentUser.Name, CurrentPassword))
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Wrong Password", "OK");
            return;
        }
        if (!Password.Equals(RePassword))
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must match", "OK");
            return;
        }

        if (!Password.IsValidPassword())
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must be at least 8 characters", "OK");
            return;
        }

        (user.Salt, user.PasswordHash) = passwordUtilities.HashPassword(Password);
        // TODO: Add new password to database.
        await popupService.ClosePopupAsync();
    }
}
