using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.App.Services;
using w3tools.App.Settings;

namespace w3tools.App
{
    
    using Services;
    using ViewModels;

    public class AppModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IConfigProvider>().To<SettingsConfigProvider>();
        }
    }
}
