using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddSingleton<MainWindowViewModel>();
        }
    }
}
