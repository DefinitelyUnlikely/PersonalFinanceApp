using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Finance.Data.Interfaces;
using Finance.Models;
using Finance.Utilities;

namespace Finance.ViewModels;

public partial class UsernamePopupViewModel : ObservableObject
{
    private readonly IPopupService popupService;
    private readonly IUserRepository userRepo;
    private readonly IPasswordUtilities passwordUtilities;

    [ObservableProperty]
    string newUsername = string.Empty;
    [ObservableProperty]
    string password = string.Empty;

    public UsernamePopupViewModel(IPopupService ps, IUserRepository ur, IPasswordUtilities pu)
    {
        userRepo = ur;
        popupService = ps;
        passwordUtilities = pu;
    }

    [RelayCommand]
    async Task ChangeUsername()
    {

        User? user = await userRepo.GetUserAsync(userRepo.CurrentUser!.UserName);

        if (user is null)
        {
            await Shell.Current.DisplayAlert("Internal User Error", "CurrentUser is null\n", "OK");
            return;
        }

        if (NewUsername is "" || Password is "")
        {
            NewUsername = string.Empty;
            Password = string.Empty;
            await Shell.Current.DisplayAlert("Entry error", "Please enter all fields\n", "OK");
            return;
        }

        if (!await passwordUtilities.VerifyPassword(userRepo.CurrentUser.UserName, Password))
        {
            Password = string.Empty;
            Password = string.Empty;
            await Shell.Current.DisplayAlert("Password error", "Wrong Password\n", "OK");
            return;
        }

        try
        {
            if (!await userRepo.UpdateUserAsync(user.Id, new() { { "display_name", NewUsername } }))
            {
                await Shell.Current.DisplayAlert("Update error", "Failed to update password, changes rolled back\n", "OK");
                return;
            }
            await Shell.Current.DisplayAlert("Username changed", "Username changed, You will need to log in again\n", "OK");
            userRepo.SetUser(null!);
            await popupService.ClosePopupAsync();
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Update Error", "Query could not complete: " + e.Message + "\n", "OK");
        }
    }
}
