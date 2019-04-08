using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.Commands
{
    /// <summary>
    /// Abstract Radish Batch Command Parentclass
    /// </summary>
    [Serializable]
    public abstract class RAD_wf_Command : WorkflowItem
    {
        //fixme bind to settings
        public bool Enabled { get; set; } = true;
       

        public RAD_wf_Command(WccCommandCategory defaultCategory = WccCommandCategory.WF_Radish)
        {
            base.Category = defaultCategory;
            base.DefaultCategory = defaultCategory;
        }

        /// <summary>
        /// base Checks on run the batch command
        /// </summary>
        /// <returns></returns>
        public override WFR Run()
        {
            //check if WorkflowItem.Run returns 1
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // all radish commands check if radish setttings are OK
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (settings.CheckSelf())
                return WFR.WFR_Error;
            //manual override
            if (!Enabled)
                return WFR.WFR_NotRun;

            // no errors detected
            return WFR.WFR_Finished;
        }
    }
}
