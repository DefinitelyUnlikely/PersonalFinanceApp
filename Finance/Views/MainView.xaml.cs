using System.Windows.Input;
using Finance.ViewModels;

namespace Finance.Views;

public partial class MainView : ContentPage
{
	public MainView(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void OnCreateClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(CreateAccView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}

	//  Placeholder, Will be replaced with MainViewModel function.
	private async void OnLoginClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(TransactionView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}
}