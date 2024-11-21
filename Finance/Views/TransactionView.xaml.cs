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
            userRepo.ResetUser();
            await Shell.Current.GoToAsync($"///{nameof(MainView)}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Navigation Error", ex.Message, "OK");
        }

    }

    protected override bool OnBackButtonPressed()
    {
        // Reset user when going back from transaction page
        userRepo.ResetUser();
        Shell.Current.GoToAsync($"///{nameof(MainView)}");
        return true;
    }


}

