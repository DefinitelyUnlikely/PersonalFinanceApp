using Finance.View;

namespace Finance
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SortView), typeof(SortView));

            // Routes for income and expense
            Routing.RegisterRoute(nameof(IncomeView), typeof(IncomeView));
            Routing.RegisterRoute(nameof(ExpenseView), typeof(ExpenseView));

            //Routes for detail and deleting

            // Routes for our Sort options
            Routing.RegisterRoute(nameof(YearView), typeof(YearView));
            Routing.RegisterRoute(nameof(MonthView), typeof(MonthView));
            Routing.RegisterRoute(nameof(WeekView), typeof(WeekView));
            Routing.RegisterRoute(nameof(DayView), typeof(DayView));
        }
    }
}
