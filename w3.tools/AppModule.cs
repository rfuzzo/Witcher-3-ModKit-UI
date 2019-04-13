using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.App.Services;

namespace w3tools.App
{
    
    using Services;
    using ViewModels;
    using w3tools.Services;

    public class AppModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IConfigService>().To<ConfigService>().InSingletonScope();
            //Bind<IConfigService>().To<ConfigService>().WhenInjectedInto<RAD_Task>();


            //Bind<ILoggerService>().To<LoggerService>().WhenInjectedInto<RAD_Task>();
            Bind<ILoggerService>().To<LoggerService>().InSingletonScope();
        }
    }
}
