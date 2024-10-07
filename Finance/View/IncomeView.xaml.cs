using Finance.ViewModel;

namespace Finance.View;

public partial class IncomeView : ContentPage
{
	public IncomeView()
	{
		InitializeComponent();
		BindingContext = new IncomeViewModel();
	}
}