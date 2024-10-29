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

        }
    }
}
