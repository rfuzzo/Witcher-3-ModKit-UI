using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.App.ViewModels;

namespace w3tools.UI
{
    /// <summary>
    /// The UI module used for the app.
    /// </summary>
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            //Dialogues
            //Bind<IDialogView>().To<DialogView>();
            //Bind<IDialogService>().To<ViewDialogService>();

            //avalon dock //FIXME
            //Bind<ILayoutSerializer>().To<LayoutSerializer>();

            Bind<MainWindow>().ToSelf().InSingletonScope();

            Bind<IViewModel>().To<MainViewModel>().WhenInjectedInto<MainWindow>();
            //Bind<IViewModel>().To<SettingsViewModel>().WhenInjectedInto<SettingsView>();

        }
    }
}
