﻿using Microsoft.Win32;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.IO;
using System.Windows;
using wcc.core;
using wcc_lite_gui_wpf.Views;
using wcc_lite_gui_wpf.ViewModels;
using wcc_lite_gui_wpf;
using w3.tools;

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
            
            InitAppSetting();

            base.OnStartup(e);
            MainWindow = Kernel.Get<MainWindow>();
            MainWindow.Show();

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

            //Bind<IViewModel>().To<CommandsListViewModel>().WhenInjectedInto<CommandsView>();
            
            //Bind<IViewModel>().To<AboutViewModel>().WhenInjectedInto<AboutView>();

        

        }
    }


}
