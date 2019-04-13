using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.App.Services;
using w3tools.App.ViewModels;


namespace w3tools.UI
{
    using Services;
    using Views.Dialogs;
    using w3tools.Services;
    using w3tools.UI.Views;

    /// <summary>
    /// The UI module used for the app.
    /// </summary>
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            //Dialoges
            Bind<IDialogView>().To<DialogView>();
            Bind<IDialogService>().To<ViewDialogService>();

            //avalon dock //FIXME
            //Bind<ILayoutSerializer>().To<LayoutSerializer>();

            //Bind<ICloseable>().To<MainWindow>().InSingletonScope(); //FIXME
            Bind<MainWindow>().ToSelf().InSingletonScope();

            Bind<IViewModel>().To<MainViewModel>().WhenInjectedInto<MainWindow>();
            //Bind<ILoggerService>().To<LogViewModel>().WhenInjectedInto<LogView>();
            //Bind<ILoggerService>().To<LogViewModel>().WhenInjectedInto<LogView>();

            //Bind<IViewModel>().To<SettingsViewModel>().WhenInjectedInto<SettingsView>();

        }
    }
}
