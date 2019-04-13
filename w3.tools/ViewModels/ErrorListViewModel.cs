using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.Services;

namespace w3tools.App.ViewModels
{
    public class ErrorListViewModel : DockableViewModel
    {
        public ErrorListViewModel(ILoggerService loggerService)
        {

            LoggerService = loggerService;
            

        }
        [Inject]
        public ILoggerService LoggerService { get; set; }
    }
}
