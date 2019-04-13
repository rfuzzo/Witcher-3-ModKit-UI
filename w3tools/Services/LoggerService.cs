using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.Services
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class LoggerService : ObservableObject, ILoggerService
    {
        public LoggerService()
        {
            ELogger = new WccExtendedLogger();
        }

        public WccExtendedLogger ELogger { get; set; }

        public string Log { get; set; }

        public void LogString(string value)
        {

            Log += value + "\r\n";
            OnPropertyChanged("Log");
        }

        public override string ToString()
        {
            return Log;
        }

    }
}
