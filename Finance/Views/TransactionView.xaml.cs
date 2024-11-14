using Finance.Managers;
using Finance.Models;
using Finance.ViewModels;
using Windows.Graphics.Display;

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
        UserManager.CurrentUser = null;
        Console.WriteLine("did we do it?");
        // return true if we want to handle the navigation ourselves.
        return false;
    }


}

