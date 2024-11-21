using Finance.Data.Database;

namespace Finance;

public partial class App : Application
{

    private readonly IFinanceDatabase database;

    public App(IFinanceDatabase database)
    {
        this.database = database;
        InitializeComponent();
        InitializeDatabase();

        MainPage = new AppShell();
    }

    private async void InitializeDatabase()
    {

        await database.InitializeDatabase();

    }

}

