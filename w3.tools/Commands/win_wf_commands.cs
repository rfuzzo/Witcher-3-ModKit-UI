using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core;
using w3.workflow;

namespace w3.tools.Commands
{
    [Serializable]
    public abstract class WIN_wf_Command : RAD_wf_Command
    {

        public WIN_wf_Command(WccCommandCategory defaultCategory = WccCommandCategory.Windows)
        {
            base.Category = defaultCategory;
            base.DefaultCategory = defaultCategory;
        }


        public override int Run()
        {
            return base.Run();
        }






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


    /// <summary>
    /// CLEANUP OF UNCOOKED, COOKED, DLC TARGET FOLDERS
    /// </summary>
    [Serializable]
    public class CleanupFolder : WIN_wf_Command
    {
        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _CleanupFolder();
        }

        private int _CleanupFolder()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                Cleanup(settings.DIR_UNCOOKED());
                Cleanup(settings.DIR_COOKED_DLC());
                Cleanup(settings.DIR_DLC());
                Cleanup(settings.DIR_MOD());
                Cleanup(settings.DIR_UNCOOKED());
                Cleanup(settings.DIR_TMP());           
            }
            catch (Exception)
            {
                //FIXME
                return -1;
                throw;
            }

            return 1;
        }

    }

    /// <summary>
    /// DEPLOYING MOD
    /// </summary>
    [Serializable]
    public class DeployModScripts : WIN_wf_Command
    {
        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _DeployModScripts();
        }

        private int _DeployModScripts()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                if (Directory.Exists(settings.DIR_MOD()))
                {
                    // deleting ALL files in %DIR_MOD_CONTENT%\scripts
                    if (!settings.auto_delete_mod)
                    {
                        //confirmation
                        //FIXME message box
                    }
                    else
                    {
                        Cleanup($"{settings.DIR_MOD_CONTENT()}\\scripts");
                    }
                }

                // copying files to %DIR_MOD%
                DirectoryInfo dirInfo = new DirectoryInfo(settings.DIR_MOD_SCRIPTS());
                FileInfo[] fileInfos = dirInfo.GetFiles();
                foreach (FileInfo file in fileInfos)
                {
                    File.Copy(file.FullName, Path.Combine(settings.DIR_MOD_CONTENT(), @"\scripts\" + file.Name), true);
                }

            }
            catch (Exception)
            {
                //FIXME
                return -1;
                throw;
            }

            return 1;
        }

    }

    /// <summary>
    /// DEPLOYING TMP MOD
    /// </summary>
    [Serializable]
    public class DeployTmpModScripts : WIN_wf_Command
    {
        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _DeployTmpModScripts();
        }

        private int _DeployTmpModScripts()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                if (Directory.Exists(settings.DIR_MOD()))
                {
                    // deleting ALL files in %DIR_TMP_MOD%
                    if (!settings.auto_delete_mod)
                    {
                        //confirmation
                        //FIXME message box
                    }
                    else
                    {
                        Cleanup(settings.DIR_TMP_MOD());
                    }
                }

                // copying files to %DIR_TMP_MOD%
                DirectoryInfo dirInfo = new DirectoryInfo(settings.DIR_TMP_MOD_SCRIPTS());
                FileInfo[] fileInfos = dirInfo.GetFiles();
                foreach (FileInfo file in fileInfos)
                {
                    File.Copy(file.FullName, Path.Combine(settings.DIR_TMP_MOD_CONTENT(), @"\scripts\" + file.Name), true);
                }

            }
            catch (Exception)
            {
                //FIXME
                return -1;
                throw;
            }

            return 1;
        }
    }
}
