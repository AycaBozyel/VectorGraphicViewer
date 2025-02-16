using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using VectorGraphicViewer.Interface;
using VectorGraphicViewer.Logging;
using VectorGraphicViewer.Service;

namespace VectorGraphicViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IShapeLoaderService, ShapeLoaderService>();  
            services.AddSingleton<IDrawingService, DrawingService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<MainWindow>();  
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var logService = _serviceProvider.GetRequiredService<ILogService>();
            logService.Log("Logging is started");

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
