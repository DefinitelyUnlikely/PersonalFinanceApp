using CommunityToolkit.Maui.Views;
using Finance.ViewModels;

namespace Finance.Views;

public partial class UsernamePopup : Popup
{
	public UsernamePopup(UsernamePopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}