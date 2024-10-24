using Microsoft.Extensions.Logging;
using Finance.ViewModel;
using Finance.View;
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
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<TransactionDatabase>();

            builder.Services.AddTransient<IncomeView>();
            builder.Services.AddTransient<IncomeViewModel>();

            builder.Services.AddTransient<ExpenseView>();
            builder.Services.AddTransient<ExpenseViewModel>();

            builder.Services.AddTransient<SortView>();
            builder.Services.AddTransient<SortViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
