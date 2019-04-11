using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.App.Services
{

	
    public interface IConfigProvider
    {
        string GetConfigSetting(string configKey);

        void SetConfigSetting(string configKey, string value);

        bool Save();

        bool Load();
    }
}
