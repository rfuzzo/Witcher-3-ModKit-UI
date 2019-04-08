using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core;
using wcc.core.Commands;
using w3.workflow;

namespace w3.tools.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class RAD_wf_Command : WorkflowItem
    {
        public RAD_wf_Command(WccCommandCategory defaultCategory = WccCommandCategory.Radish)
        {
            base.Category = defaultCategory;
            base.DefaultCategory = defaultCategory;
        }


        public override int Run()
        {
            //check if WorkflowItem.Run returns 1
            if (base.Run() != 1)
            {
                //catch some error //FIXME

                //break //FIXME
                return -1;
            }

            // all radish commands check if radish setttings are OK
            if (CheckRadishSettings() != 1)
            {
                return -1;
            }

            // no errors detected
            return 1;
        }

        private int CheckRadishSettings()
        {

            RAD_Settings settings = (RAD_Settings)base.Parent;

            bool test_DIR_W3 = Directory.Exists(settings.DIR_W3);
            bool test_DIR_MODKIT = Directory.Exists(settings.DIR_MODKIT);
            bool test_DIR_ENCODER = Directory.Exists(settings.DIR_ENCODER);

            bool test_DIR_PROJECT_BASE = Directory.Exists(settings.DIR_PROJECT_BASE);
            bool test_MODNAME = !String.IsNullOrEmpty(settings.MODNAME);
            bool test_idspace = settings.idspace > 0 && settings.idspace < 9999;


            if (!(test_DIR_W3 && test_DIR_MODKIT && test_DIR_ENCODER))
            {
                // check global settings
                // FIXME Error Message
                return -1;
            }
            else if (!(test_DIR_PROJECT_BASE && test_MODNAME && test_idspace))
            {
                //check mod settings
                // FIXME Error Message
                return 0;
            }
            else
            {
                // all true
                return 1;
            }
        }
    }
}
