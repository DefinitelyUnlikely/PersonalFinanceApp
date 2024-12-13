using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class PasswordPopupViewModel : ObservableObject
{
    private readonly IPopupService popupService;
    private readonly IUserRepository userRepo;
    private readonly IPasswordUtilities passwordUtilities;

    public PasswordPopupViewModel(IPopupService ps, IUserRepository ur, IPasswordUtilities pu)
    {
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

        User? user = await userRepo.GetUserAsync(userRepo.CurrentUser!.UserName);

        if (user is null)
        {
            await Shell.Current.DisplayAlert("Internal User Error", "CurrentUser is null\n", "OK");
            return;
        }

        if (CurrentPassword is "" || Password is "" || RePassword is "")
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Entry error", "Please enter all fields\n", "OK");
            return;
        }

        if (!await passwordUtilities.VerifyPassword(userRepo.CurrentUser.UserName, CurrentPassword))
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Wrong Password\n", "OK");
            return;
        }
        if (!Password.Equals(RePassword))
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must match\n", "OK");
            return;
        }

        if (!Password.IsValidPassword())
        {
            CurrentPassword = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Passwords must be at least 8 characters\n", "OK");
            return;
        }

        try
        {
            (var newSalt, var newPass) = passwordUtilities.HashPassword(Password);
            if (!await userRepo.UpdateUserAsync(user.Id, new() { { "salt", newSalt }, { "password", newPass } }))
            {
                await Shell.Current.DisplayAlert("Update error", "Failed to update password, changes rolled back\n", "OK");
                return;
            }
            user.Salt = newSalt;
            user.PasswordHash = newPass;
            await Shell.Current.DisplayAlert("Password changed", "Password changed, You will need to log in again\n", "OK");
            userRepo.SetUser(null!);
            await popupService.ClosePopupAsync();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Update Error", "Something went wrong: " + e.Message + "\n", "OK");
        }

    }
}
