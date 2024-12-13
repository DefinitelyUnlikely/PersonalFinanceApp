using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Views;
using Npgsql;

namespace Finance.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    private readonly IUserRepository userRepo;
    private readonly IAccountRepository accountRepo;
    private readonly ITransactionRepository transactionRepo;
    private readonly IPopupService popupService;

    [ObservableProperty]
    Account? selectedAccount;

    [ObservableProperty]
    ObservableCollection<Account> accounts = [];

    [ObservableProperty]
    string? userName;

    [ObservableProperty]
    string? displayName;

    public AccountViewModel(IUserRepository ur, IAccountRepository ar, ITransactionRepository tr, IPopupService pu)
    {
        userRepo = ur;
        accountRepo = ar;
        transactionRepo = tr;
        popupService = pu;

        DisplayName = userRepo.CurrentUser!.DisplayName;
        UserName = userRepo.CurrentUser!.UserName;

        LoadAccounts();
    }

    async void LoadAccounts()
    {
        if (userRepo.CurrentUser is null)
        {
            throw new Exception("User is null, something has gone wrong.\n");
        }

        try
        {
            Accounts = new ObservableCollection<Account>(await accountRepo.GetUserAccountsAsync());
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Account error", "Could not load accounts\n" + e.Message, "OK");
            return;
        }
    }

    [RelayCommand]
    async Task AccountDetails(Account account)
    {
        SelectedAccount = account;
        accountRepo.SetAccount(SelectedAccount.Id);
        await Shell.Current.GoToAsync($"{nameof(TransactionView)}");
    }

    [RelayCommand]
    async Task ShowAllTransactions()
    {
        accountRepo.SetAccount(null);
        await Shell.Current.GoToAsync($"{nameof(TransactionView)}");
    }

    [RelayCommand]
    public async Task CreateAccount()
    {
        try
        {
            await popupService.ShowPopupAsync<AccountPopupViewModel>();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message + "\n", "OK");
        }
    }

    [RelayCommand]
    public async Task ChangeUsername()
    {
        try
        {
            await popupService.ShowPopupAsync<UsernamePopupViewModel>();
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");

        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message + "\n", "OK");
        }

    }

    [RelayCommand]
    public async Task ChangePassword()
    {
        try
        {
            await popupService.ShowPopupAsync<PasswordPopupViewModel>();
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Error", e.Message + "\n", "OK");
        }

    }

}
