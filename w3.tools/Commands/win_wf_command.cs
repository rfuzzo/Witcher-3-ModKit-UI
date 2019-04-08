using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3.workflow;

namespace w3.tools.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class WIN_wf_Command : RAD_wf_Command
    {

        public WIN_wf_Command(WccCommandCategory defaultCategory = WccCommandCategory.WF_Windows)
        {
            base.Category = defaultCategory;
            base.DefaultCategory = defaultCategory;
        }


        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public void Cleanup(string v)
        {
            if (Directory.Exists(v))
            {
                ClearFolder(v);
            }
            else
            {
                Directory.CreateDirectory(v);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FolderName"></param>
        public void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                di.Delete();
            }
        }
    }
}
