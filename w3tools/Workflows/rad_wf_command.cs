using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;
using w3tools.Services;

namespace w3tools.Workflows
{
    /// <summary>
    /// Abstract Radish Batch Command Parentclass
    /// </summary>
    [Serializable]
    public abstract class RAD_wf_Command : ObservableObject, IWorkflowItem
    {
        public RAD_Task RAD_Task { get; set; }

        [BrowsableAttribute(false)]
        public string Name { get; set; }
        [BrowsableAttribute(false)]
        public string Image { get; } = "/w3tools.UI;component/Resources/radish_icon_circle.png";
        [BrowsableAttribute(false)]
        public bool IsVisible { get; set; } = true;
        [BrowsableAttribute(false)]
        public object Parent { get; set; }


        /// <summary>
        /// base Checks on run the batch command
        /// </summary>
        /// <returns></returns>
        public virtual WFR Run()
        {
            // all radish commands check if radish setttings are OK
            WF_Settings settings = (WF_Settings)Parent;
            if (!settings.CheckSelf())
                return WFR.WFR_Error;

            // no errors detected
            return WFR.WFR_Finished;
        }

        public override string ToString() => Name;
    }
}
