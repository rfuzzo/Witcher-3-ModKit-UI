using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.Workflows
{
    /// <summary>
    /// Abstract Wcc_lite Batch Command Parentclass
    /// </summary>
    [Serializable]
    public abstract class WCC_wf_Command : WorkflowItem
    {
        [Browsable(false)]
        public new string Image { get; set; } = "/w3tools.UI;component/Resources/witcher3_101.ico";
        public WCC_Task WCC_Task { get; set; }

        /// <summary>
        /// base Checks on run the batch command
        /// </summary>
        /// <returns></returns>
        public virtual WFR Run()
        {
            // all radish commands check if radish setttings are OK
            WF_Settings settings = (WF_Settings)CustomTag;
            if (!settings.CheckSelf())
                return WFR.WFR_Error;

            // no errors detected
            return WFR.WFR_Finished;
        }

        public override string ToString() => Name;
    }
}
