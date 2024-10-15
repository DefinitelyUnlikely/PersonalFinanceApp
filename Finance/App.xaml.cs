namespace Finance
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //             Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            //             {
            // #if WINDOWS
            //             var mauiWindow = handler.VirtualView;
            //             var nativeWindow = handler.PlatformView;
            //             nativeWindow.Activate();
            //             IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            //             var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            //             var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            //             appWindow.Resize(new Windows.Graphics.SizeInt32(850, 600));
            // #endif
            //             });
        }
    }
}
