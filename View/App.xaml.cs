using Serilog;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Приложение запущено");
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            Log.Information("Приложение закрыто");
            await Log.CloseAndFlushAsync();
            base.OnExit(e);
        }
    }
}
