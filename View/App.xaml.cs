﻿using Data.Abstractions;
using Data.Context;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Windows;
using ViewModels;
using ViewModels.Services;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider? serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Приложение запущено");
            try
            {
                var services = new ServiceCollection();
                services.AddSingleton<SQLiteDBContext>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<ICompanyRepository, CompanyRepository>();
                services.AddScoped<DataService>();
                services.AddSingleton<MainWindowVM>();

                serviceProvider = services.BuildServiceProvider();

                Log.Information("Все сервисы успешно загружены.");

                var mainWindow = new MainWindow()
                {
                    DataContext = serviceProvider?.GetService<MainWindowVM>()
                };
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка загрузки сервисов.");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            Log.Information("Приложение закрыто");
            await Log.CloseAndFlushAsync();
            base.OnExit(e);
        }
    }
}
