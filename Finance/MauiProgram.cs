using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui;
using Finance.ViewModels;
using Finance.Views;
using Finance.Data;

namespace Finance
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<CreateAccView>();
            builder.Services.AddTransient<CreateAccViewModel>();

            builder.Services.AddTransient<TransactionView>();
            builder.Services.AddTransient<TransactionViewModel>();
            builder.Services.AddSingleton<FinanceDatabase>();

            builder.Services.AddTransient<IncomeView>();
            builder.Services.AddTransient<IncomeViewModel>();

            builder.Services.AddTransient<ExpenseView>();
            builder.Services.AddTransient<ExpenseViewModel>();

            builder.Services.AddTransient<SortView>();
            builder.Services.AddTransient<SortViewModel>();

            builder.Services.AddTransientPopup<PasswordPopup, PasswordPopupViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
