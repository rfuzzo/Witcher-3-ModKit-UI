using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using w3tools.App.Commands;
using w3tools.App.Services;

namespace w3tools.App.ViewModels.Dialogs
{
    public class AboutDialogViewModel : DialogViewModel
    {

        public AboutDialogViewModel()
        {
            OKCommand = new RelayCommand(OK);
            OpenGithubCommand = new RelayCommand(() => Process.Start(URLGithub));
        }


        public string URLGithub { get; } = "https://github.com/rfuzzo/Witcher-3-ModKit-UI";
        public Version Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version;



        public ICommand OpenGithubCommand { get; }


        public ICommand OKCommand { get; }

       

        private void OK()
        {
            InvokeDialogCloseRequest(true);
        }
    }
}
