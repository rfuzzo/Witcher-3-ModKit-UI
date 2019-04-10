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

            //Kernel.Load<CoreModule>();
            Kernel.Load<AppModule>();
            Kernel.Load<UIModule>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = Kernel.Get<MainWindow>();
            MainWindow.Show();
        }






        /*private void InitAppSetting()
        {
            //AppSettings = new AppSettings();

            //debug //FIXME
            AppSettings.GamePath = @"E:\moddingdir_tw3\The Witcher 3\bin\x64\witcher3.exe";
            AppSettings.ToolsPath = @"E:\\moddingdir_tw3\TOOLS\Encoder";
            AppSettings.ModKitPath = @"E:\moddingdir_tw3\TOOLS\Modkit\patch1337bugfix\bin\x64\wcc_lite.exe";
            //debug


            //Wcc Comamnds
            if (AppSettings.WCC_Commands == null)
            {
                AppSettings.WCC_Commands = new WccCommandsCollection();
            }

            //Wcc Path
            if (AppSettings.ModKitPath == null || !File.Exists(AppSettings.ModKitPath))
            {
                var fd = new OpenFileDialog
                {
                    Title = "Select wcc_lite.exe.",
                    FileName = AppSettings.ModKitPath,
                    Filter = "wcc_lite.exe|wcc_lite.exe"
                };
                if (fd.ShowDialog() == true && fd.CheckFileExists)
                {
                    AppSettings.ModKitPath = fd.FileName;
                }
            }

            w3tools.Properties.Settings.Default.Save();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            w3tools.Properties.Settings.Default.Save();
        }*/


    }

    /// <summary>
    /// The standard module used for the app.
    /// </summary>
    /*public class StandardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf().InSingletonScope();
            Bind<MainWindow>().ToSelf().InSingletonScope();
            Bind<IViewModel>().To<MainViewModel>().WhenInjectedInto<MainWindow>();

            //SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
            //Bind<IDialogService>().To<DialogService>().WhenInjectedInto<UtilitiesViewModel>();
            //Bind<IDialogService>().ToSelf().InSingletonScope();

            //3) Injection of dependency IDialogFactory into parameter dialogFactory of constructor of type DialogService
            //2) Injection of dependency IDialogService into parameter dialogService of constructor of type UtilitiesViewModel
            //1) Request for UtilitiesViewModel

            //IConfigProvider config = new SettingsConfigProvider();
            Bind<IConfigProvider>().To<SettingsConfigProvider>();

        }
    }*/


}
