using System.Windows;
using Autofac;
using CineQuebec.Windows.DAL;
using CineQuebec.Windows.DAL.Interfaces;
using CineQuebec.Windows.DAL.Providers;
using CineQuebec.Windows.View;

namespace CineQuebec.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // protected override void OnStartup(StartupEventArgs e)
        // {
        //     base.OnStartup(e);
        //     var builder = new ContainerBuilder();
        //     builder.RegisterType<DatabaseProvider>().As<IDatabaseProvider>().SingleInstance();
        //     var container = builder.Build();
        //     var mainWindow = container.Resolve<MainWindow>();
        //     mainWindow.Show();
        // }
    }
}