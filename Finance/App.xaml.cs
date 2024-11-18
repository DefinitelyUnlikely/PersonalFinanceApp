using Finance.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance;

public partial class App : Application
{
    public App()
    {

        InitializeComponent();
        InitializeDatabase();

        MainPage = new AppShell();
    }

    private async void InitializeDatabase()
    {

        await FinanceDatabase.CreateTablesIfNotExists();

    }

}

