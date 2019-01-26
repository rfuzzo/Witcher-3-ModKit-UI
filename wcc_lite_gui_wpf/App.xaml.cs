using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.Windows;
using Wcc_lite_core;
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

            if (wcc_lite_gui_wpf.Properties.Settings.Default.WccLite_Commands == null)
            {
                wcc_lite_gui_wpf.Properties.Settings.Default.WccLite_Commands = new WccCommandsCollection();
            }
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
            Bind<MainWindow>().ToProvider<MainViewProvider>().InSingletonScope();

            
        }
    }

    /// <summary>
    /// The main view provider for use with the kernel.
    /// </summary>
    public class MainViewProvider : Provider<MainWindow>
    {
        protected override MainWindow CreateInstance(IContext context)
        {
            var viewModel = context.Kernel.Get<MainViewModel>();
            var view = new MainWindow(viewModel);
            //var view = new MainWindow();
            return view;
        }
    }
}
