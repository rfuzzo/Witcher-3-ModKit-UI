using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.Services;

namespace w3tools.App.ViewModels
{
    public class LogViewModel : DockableViewModel
    {
        public LogViewModel( ILoggerService extendedLogger)
        {

            ExtendedLogger = (LoggerService)extendedLogger;
            MyProperty = "beghin";

            MyProperty = ExtendedLogger.Log;

        }

        public LoggerService ExtendedLogger { get; set; }

        private string myVar;
        public string MyProperty
        {
            get => myVar;
            set
            {
                if (myVar != value)
                {
                    myVar = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
