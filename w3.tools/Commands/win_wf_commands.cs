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
    /// CLEANUP OF UNCOOKED, COOKED, DLC TARGET FOLDERS
    /// </summary>
    [Serializable]
    public class CleanupFolder : WIN_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.FULL_REBUILD)
                return (int)WFR.WFR_NotRun;

            WFR result = _CleanupFolder(settings);
            return result;
        }

        private WFR _CleanupFolder(RAD_Settings settings)
        {
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
                return WFR.WFR_Error;
                throw;
            }

            return WFR.WFR_Finished;
        }

    }

    /// <summary>
    /// DEPLOYING MOD
    /// </summary>
    [Serializable]
    public class DeployModScripts : WIN_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.DEPLOY_SCRIPTS)
                return (int)WFR.WFR_NotRun;

            WFR result = _DeployModScripts(settings);
            return result;
        }

        private WFR _DeployModScripts(RAD_Settings settings)
        {
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
                return WFR.WFR_Error;
                throw;
            }

            return WFR.WFR_Finished;
        }

    }

    /// <summary>
    /// DEPLOYING TMP MOD
    /// </summary>
    [Serializable]
    public class DeployTmpModScripts : WIN_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.DEPLOY_TMP_SCRIPTS)
                return (int)WFR.WFR_NotRun;

            WFR result = _DeployTmpModScripts(settings);
            return result;
        }

        private WFR _DeployTmpModScripts(RAD_Settings settings)
        {
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
                return WFR.WFR_Error;
                throw;
            }

            return WFR.WFR_Finished;
        }
    }

    /// <summary>
    /// PREPARE COOKING: CLEANUP OF UNCOOKED CONTENT
    /// </summary>
    [Serializable]
    public class PrepareCooking : WIN_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_COOK)
                return (int)WFR.WFR_NotRun;

            WFR result = _PrepareCooking(settings);
            return result;
        }

        private WFR _PrepareCooking(RAD_Settings settings)
        {
            try
            {
                if (!Directory.Exists(settings.DIR_TMP()))
                    Directory.CreateDirectory(settings.DIR_TMP());
            }
            catch (Exception)
            {
                //FIXME
                return WFR.WFR_Error;
                throw;
            }

            return WFR.WFR_Finished;
        }
    }

    /// <summary>
    /// PREPARE PACKING: COPY ADDITIONAL DATA
    /// </summary>
    [Serializable]
    public class PreparePackaging : WIN_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_REPACK_DLC)
                return (int)WFR.WFR_NotRun;

            WFR result = _PreparePackaging(settings);
            return result;
        }

        private WFR _PreparePackaging(RAD_Settings settings)
        {
            try
            {
                // LOG copying files to %DIR_COOKED_DLC%
                var files = Directory.GetFiles(Path.Combine(settings.DIR_OUTPUT_QUEST(), "dlc"), "*.w3hub", SearchOption.TopDirectoryOnly);
                foreach (var item in files)
                {
                    string filename = Path.GetFileName(item);
                    string newpath = Path.Combine(settings.DIR_COOKED_DLC(), "dlc",filename);
                    File.Copy(item, newpath);
                }

                //copy additonal files
                Directory.Move(Path.Combine(settings.DIR_PROJECT_BASE, "additional"), settings.DIR_COOKED_DLC());
                
            }
            catch (Exception)
            {
                //FIXME
                return WFR.WFR_Error;
                throw;
            }

            return WFR.WFR_Finished;
        }
    }
}
