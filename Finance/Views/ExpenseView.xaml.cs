using Finance.ViewModels;

namespace Finance.Views;

public partial class ExpenseView : ContentPage
{
	public ExpenseView(ExpenseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}