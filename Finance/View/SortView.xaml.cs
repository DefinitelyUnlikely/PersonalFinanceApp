namespace Finance.View;

public partial class SortView : ContentPage
{
	public SortView()
	{
		InitializeComponent();
	}

	// Implementera On...Clicked.
	private async void OnYearClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(YearView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}

	private async void OnMonthClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(MonthView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}

	private async void OnWeekClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(WeekView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}

	private async void OnDayClicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(DayView));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Navigation Error", ex.Message, "OK");
		}
	}
}