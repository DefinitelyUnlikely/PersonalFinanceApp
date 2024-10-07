using Finance.ViewModel;

namespace Finance.View;

public partial class ExpenseView : ContentPage
{
	public ExpenseView(ExpenseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}