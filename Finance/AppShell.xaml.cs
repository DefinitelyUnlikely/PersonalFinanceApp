using Finance.Views;

namespace Finance
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Route for account creation
            Routing.RegisterRoute(nameof(CreateAccView), typeof(CreateAccView));

            // Routes for transactions and sorting
            Routing.RegisterRoute(nameof(TransactionView), typeof(TransactionView));
            Routing.RegisterRoute(nameof(SortView), typeof(SortView));

            // Routes for income and expense
            Routing.RegisterRoute(nameof(IncomeView), typeof(IncomeView));
            Routing.RegisterRoute(nameof(ExpenseView), typeof(ExpenseView));

        }
    }
}
