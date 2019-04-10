using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.App.Services
{

	
    public interface IConfigProvider
    {
        string Host { get; set; }
        int Port { get; set; }

        void Save();
        void Load();
    }
}
