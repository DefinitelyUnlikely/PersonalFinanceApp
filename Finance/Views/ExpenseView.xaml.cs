using Finance.ViewModels;

namespace Finance.Views;

public partial class ExpenseView : ContentPage
{
	public ExpenseView(ExpenseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override bool OnBackButtonPressed()
	{
		Shell.Current.GoToAsync($"/{nameof(TransactionView)}");
		return true;
	}
}