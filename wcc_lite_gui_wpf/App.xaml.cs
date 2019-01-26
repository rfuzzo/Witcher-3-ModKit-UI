using Microsoft.Win32;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.IO;
using System.Windows;
using Wcc_lite_core;
using wcc_lite_gui_wpf.Forms;
using wcc_lite_gui_wpf.ViewModels;

namespace wcc_lite_gui_wpf
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
            base.OnStartup(e);
            MainWindow = Kernel.Get<MainWindow>();
            MainWindow.Show();

            InitAppSetting();
        }

        private void InitAppSetting()
        {
            //Wcc Comamnds
            if (wcc_lite_gui_wpf.Properties.Settings.Default.WccLite_Commands == null)
            {
                wcc_lite_gui_wpf.Properties.Settings.Default.WccLite_Commands = new WccCommandsCollection();
            }

            //Wcc Path
            if (wcc_lite_gui_wpf.Properties.Settings.Default.WccPath == null || !File.Exists(wcc_lite_gui_wpf.Properties.Settings.Default.WccPath))
            {
                var fd = new OpenFileDialog
                {
                    Title = "Select wcc_lite.exe.",
                    FileName = wcc_lite_gui_wpf.Properties.Settings.Default.WccPath,
                    Filter = "wcc_lite.exe|wcc_lite.exe"
                };
                if (fd.ShowDialog() == true && fd.CheckFileExists)
                {
                    wcc_lite_gui_wpf.Properties.Settings.Default.WccPath = fd.FileName;
                }
            }

            wcc_lite_gui_wpf.Properties.Settings.Default.Save();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            wcc_lite_gui_wpf.Properties.Settings.Default.Save();
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
            Bind<IViewModel>().To<CommandsViewModel>().WhenInjectedInto<UCCommands>();

           

        }
    }

    /// <summary>
    /// The main view provider for use with the kernel.
    /// </summary>
    /*public class MainViewProvider : Provider<MainWindow>
    {
        protected override MainWindow CreateInstance(IContext context)
        {
            var viewModel = context.Kernel.Get<MainViewModel>();
            var view = new MainWindow(viewModel);
            return view;
        }
    }*/
}
