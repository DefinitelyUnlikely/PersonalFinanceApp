using System;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;

namespace Finance.ViewModels;

public partial class AccountPopupViewModel : ObservableObject
{
    private readonly IPopupService popupService;
    private readonly IUserRepository userRepo;
    private readonly IAccountRepository accountRepo;

    [ObservableProperty]
    string? newAccountName = string.Empty;

    public AccountPopupViewModel(IPopupService ps, IUserRepository ur, IAccountRepository ar)
    {
        popupService = ps;
        userRepo = ur;
        accountRepo = ar;
    }

    [RelayCommand]
    async Task CreateNewAccount()
    {
        if (userRepo.CurrentUser is null)
        {
            throw new Exception("User is NULL");
        }

        if (NewAccountName is null)
        {
            NewAccountName = string.Empty; // Just in case
            await Shell.Current.DisplayAlert("Account Creation", "Please enter an account name", "OK");
        }

        try
        {
            Account newAccount = new(userRepo.CurrentUser.Id, NewAccountName);
            await accountRepo.AddAccountAsync(newAccount);
            // Add to the account list in the AccountView as well
            await popupService.ClosePopupAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Create Account Error: " + e.Message);
        }
    }
}
