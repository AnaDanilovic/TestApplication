using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TestApplication.Processors;

namespace TestApplication
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IProcessFile, ProcessFile>();
            services.AddTransient<TextManagerWindow>();
        }

        [STAThread]
        public static void Main()
        {
            App app = new App();
            TextManagerWindow mainWindow = app._serviceProvider.GetService<TextManagerWindow>();
            app.Run(mainWindow);
        }
    }
}
