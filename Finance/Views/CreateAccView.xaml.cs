using Finance.ViewModels;

namespace Finance.Views;

public partial class CreateAccView : ContentPage
{
	public CreateAccView(CreateAccViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}