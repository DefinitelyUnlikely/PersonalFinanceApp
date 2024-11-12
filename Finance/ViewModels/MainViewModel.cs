using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Finance.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [RelayCommand]
    async Task Forgot(string url)
    {
        try
        {
            await Launcher.OpenAsync(url);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
