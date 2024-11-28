using Finance.Data.Interfaces;
using Finance.Data.Repositories;
using Finance.Models;
using Finance.ViewModels;

namespace Finance.Views;

public partial class TransactionView : ContentPage
{

    private readonly IUserRepository userRepo;

    public TransactionView(IUserRepository ur, TransactionViewModel vm)
    {
        InitializeComponent();
        userRepo = ur;
        BindingContext = vm;
    }

    private async void OnSortClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(SortView));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }
    }

    private async void OnFilterClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(FilterView));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }
    }

    private async void OnIncomeClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(IncomeView));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }
    }

    private async void OnExpenseClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(nameof(ExpenseView));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }
    }

    private async void OnLogoutPressed(object sender, EventArgs e)
    {
        try
        {
            bool answer = await Shell.Current.DisplayAlert("Logout", "Are you sure you want to logout?", "YES", "NO");
            if (answer)
            {
                userRepo.ResetUser();
                await Shell.Current.GoToAsync($"///{nameof(MainView)}");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }

    }

    protected override bool OnBackButtonPressed()
    {
        // Cannot ask if the user wants to log out, as a DisplayAlert will freeze the 
        // application if it is not awaited. And we cannot put this method async as it is an override.
        userRepo.ResetUser();
        Shell.Current.GoToAsync($"///{nameof(MainView)}");

        return true;
    }


}

