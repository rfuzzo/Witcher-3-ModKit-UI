using Microsoft.Win32;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.IO;
using System.Windows;
using wcc.core;
using w3tools.UI.Views;
using w3tools.UI.ViewModels;
using w3tools.UI;
using w3tools;

namespace w3tools.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IKernel Kernel { get; }

        public App() : base()
        {
            Kernel = new StandardKernel();
            Kernel.Load<StandardModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            
            InitAppSetting();

            base.OnStartup(e);
            MainWindow = Kernel.Get<MainWindow>();
            MainWindow.Show();

        }

        private void InitAppSetting()
        {
            //Wcc Comamnds
            if (w3tools.UI.Properties.Settings.Default.WccLite_Commands == null)
            {
                w3tools.UI.Properties.Settings.Default.WccLite_Commands = new WccCommandsCollection();
            }

            //Wcc Path
            if (w3tools.UI.Properties.Settings.Default.WccPath == null || !File.Exists(w3tools.UI.Properties.Settings.Default.WccPath))
            {
                var fd = new OpenFileDialog
                {
                    Title = "Select wcc_lite.exe.",
                    FileName = w3tools.UI.Properties.Settings.Default.WccPath,
                    Filter = "wcc_lite.exe|wcc_lite.exe"
                };
                if (fd.ShowDialog() == true && fd.CheckFileExists)
                {
                    w3tools.UI.Properties.Settings.Default.WccPath = fd.FileName;
                }
            }

            w3tools.UI.Properties.Settings.Default.Save();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            w3tools.UI.Properties.Settings.Default.Save();
        }
    }

    /// <summary>
    /// The standard module used for the app.
    /// </summary>
    public class StandardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf().InSingletonScope();
            Bind<MainWindow>().ToSelf().InSingletonScope();
            Bind<IViewModel>().To<MainViewModel>().WhenInjectedInto<MainWindow>();

            //Bind<IViewModel>().To<CommandsListViewModel>().WhenInjectedInto<CommandsView>();
            
            //Bind<IViewModel>().To<AboutViewModel>().WhenInjectedInto<AboutView>();

        

        }
    }


}
