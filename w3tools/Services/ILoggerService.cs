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
    public interface ILoggerService
    {
       

        string Log { get; }
        WccExtendedLogger ELogger { get; }

        void LogString(string value);
        //object LogExtended(string, configKey, string value);

    }
}
