using Finance.Managers;
using Finance.Models;
using Finance.ViewModels;

namespace Finance.Views;

public partial class TransactionView : ContentPage
{
    public TransactionView(TransactionViewModel vm)
    {
        InitializeComponent();
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

    protected override bool OnBackButtonPressed()
    {
        // Reset user when going back from transaction page
        UserManager.CurrentUser = null;
        return false;
    }


}

