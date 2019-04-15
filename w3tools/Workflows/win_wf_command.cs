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
    /// 
    /// </summary>
    [Serializable]
    public abstract class WIN_wf_Command : WorkflowItem
    {
        public new ECommandCategory Category { get; set; } = ECommandCategory.WF_Windows;
        public new bool IsVisible { get; set; } = false;

        /// <summary>
        /// 
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
