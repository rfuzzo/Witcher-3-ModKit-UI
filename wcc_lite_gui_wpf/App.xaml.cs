using Microsoft.Win32;
using MvvmDialogs;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.IO;
using System.Windows;
using w3tools.App.ViewModels;
using w3tools.App.Services;
using Ninject.Infrastructure;
using w3tools.App;

namespace w3tools.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IHaveKernel
    {
        public IKernel Kernel { get; }

        public App()
        {
            Kernel = new StandardKernel();

            Kernel.Load<CoreModule>();
            Kernel.Load<AppModule>();
            Kernel.Load<UIModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = Kernel.Get<MainWindow>();
            MainWindow.Show();
        }
    }
}
