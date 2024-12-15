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
    private readonly AccountViewModel accountViewModel;

    [ObservableProperty]
    string? newAccountName = string.Empty;

    public AccountPopupViewModel(IPopupService ps, IUserRepository ur, IAccountRepository ar, AccountViewModel avm)
    {
        popupService = ps;
        userRepo = ur;
        accountRepo = ar;
        accountViewModel = avm;
    }

    [RelayCommand]
    async Task CreateNewAccount(string name)
    {
        await accountViewModel.CreateNewAccount(name);
    }
}
