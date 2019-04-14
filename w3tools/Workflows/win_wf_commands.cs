using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.Workflows
{
   

    /// <summary>
    /// CLEANUP OF UNCOOKED, COOKED, DLC TARGET FOLDERS
    /// </summary>
    [Serializable]
    public class CleanupFolder : WIN_wf_Command
    {
        public CleanupFolder()
        {
            Name = "Cleanup Folder";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            WF_Settings settings = (WF_Settings)base.Parent;
            if (!settings.FULL_REBUILD)
                return (int)WFR.WFR_NotRun;

           return _CleanupFolder(settings);
        }

        private WFR _CleanupFolder(WF_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- CLEANUP OF UNCOOKED, COOKED, DLC TARGET FOLDERS pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                settings.LOGGER.LogString($"deleting: {settings.DIR_UNCOOKED()}");
                Cleanup(settings.DIR_UNCOOKED());

                settings.LOGGER.LogString($"deleting: {settings.DIR_COOKED_DLC()}");
                Cleanup(settings.DIR_COOKED_DLC());

                settings.LOGGER.LogString($"deleting: {settings.DIR_DLC()}");
                Cleanup(settings.DIR_DLC());

                settings.LOGGER.LogString($"deleting: {settings.DIR_MOD()}");
                Cleanup(settings.DIR_MOD());

                settings.LOGGER.LogString($"deleting: {settings.DIR_UNCOOKED()}");
                Cleanup(settings.DIR_UNCOOKED());

                settings.LOGGER.LogString($"deleting: {settings.DIR_TMP()}");
                Cleanup(settings.DIR_TMP());

                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                throw ex;
            }
        }

    }

    /// <summary>
    /// DEPLOYING MOD
    /// </summary>
    [Serializable]
    public class DeployModScripts : WIN_wf_Command
    {
        private bool auto_delete_mod {get;set;}

        public DeployModScripts()
        {
            Name = "Deploy Mod Scripts";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            WF_Settings settings = (WF_Settings)base.Parent;
            if (!settings.DEPLOY_SCRIPTS)
                return (int)WFR.WFR_NotRun;

           return _DeployModScripts(settings);
        }

        private WFR _DeployModScripts(WF_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- DEPLOYING MOD pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                settings.LOGGER.LogString($"deleting ALL files in {settings.DIR_MOD_CONTENT()}\\scripts");

                if (Directory.Exists(settings.DIR_MOD()))
                {
                    // deleting ALL files in %DIR_MOD_CONTENT%\scripts
                    if (!auto_delete_mod)
                    {
                        //confirmation
                        //FIXME message box
                    }
                    else
                    {
                        Cleanup($"{settings.DIR_MOD_CONTENT()}\\scripts");
                        settings.LOGGER.LogString($"ALL FILES DELETED");
                    }
                }

                // copying files to %DIR_MOD%
                settings.LOGGER.LogString($"copying files to {settings.DIR_MOD_CONTENT()}");
                if (Directory.Exists(settings.DIR_MOD_SCRIPTS()))
                {
                    var files = Directory.GetFiles(settings.DIR_MOD_SCRIPTS());
                    foreach (string file in files)
                    {
                        File.Copy(Path.GetFileName(file), Path.Combine(settings.DIR_MOD_CONTENT(), @"\scripts\" + Path.GetFileName(file)), true);
                    }
                }

                settings.LOGGER.LogString($"mod deployed.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                throw ex;
            }
        }

    }

    /// <summary>
    /// DEPLOYING TMP MOD
    /// </summary>
    [Serializable]
    public class DeployTmpModScripts : WIN_wf_Command
    {
        private bool auto_delete_mod { get; set; }

        public DeployTmpModScripts()
        {
            Name = "Deploy Tmp-Mod Scripts";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            WF_Settings settings = (WF_Settings)base.Parent;
            if (!settings.DEPLOY_TMP_SCRIPTS)
                return (int)WFR.WFR_NotRun;

            return _DeployTmpModScripts(settings);
        }

        private WFR _DeployTmpModScripts(WF_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- DEPLOYING TMP MOD pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                settings.LOGGER.LogString($"deleting ALL files in {settings.DIR_TMP_MOD()}\\scripts");

                if (Directory.Exists(settings.DIR_TMP_MOD()))
                {
                    // deleting ALL files in %DIR_TMP_MOD%
                    if (!auto_delete_mod)
                    {
                        //confirmation
                        //FIXME message box
                    }
                    else
                    {
                        Cleanup(settings.DIR_TMP_MOD());
                        settings.LOGGER.LogString($"ALL FILES DELETED");
                    }
                }

                // copying files to %DIR_TMP_MOD%
                settings.LOGGER.LogString($"copying files to {settings.DIR_TMP_MOD()}");
                DirectoryInfo dirInfo = new DirectoryInfo(settings.DIR_TMP_MOD_SCRIPTS());
                FileInfo[] fileInfos = dirInfo.GetFiles();
                foreach (FileInfo file in fileInfos)
                {
                    File.Copy(file.FullName, Path.Combine(settings.DIR_TMP_MOD_CONTENT(), @"\scripts\" + file.Name), true);
                }

                settings.LOGGER.LogString($"tmp-mod deployed.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                throw ex;
            }
        }
    }

    /// <summary>
    /// PREPARE COOKING: CLEANUP OF UNCOOKED CONTENT
    /// </summary>
    [Serializable]
    public class PrepareCooking : WIN_wf_Command
    {
        public PrepareCooking()
        {
            Name = "Prepare Cooking";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            WF_Settings settings = (WF_Settings)base.Parent;
            if (!settings.WCC_COOK)
                return (int)WFR.WFR_NotRun;

            return _PrepareCooking(settings);
        }

        private WFR _PrepareCooking(WF_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- PREPARE COOKING: CLEANUP OF UNCOOKED CONTENT");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                if (!Directory.Exists(settings.DIR_TMP()))
                    Directory.CreateDirectory(settings.DIR_TMP());

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                throw ex;
            }
        }
    }

    /// <summary>
    /// PREPARE PACKING: COPY ADDITIONAL DATA
    /// </summary>
    [Serializable]
    public class PreparePackaging : WIN_wf_Command
    {
        public PreparePackaging()
        {
            Name = "Prepare Packing";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            WF_Settings settings = (WF_Settings)base.Parent;
            if (!settings.WCC_REPACK_DLC)
                return (int)WFR.WFR_NotRun;

            return _PreparePackaging(settings);
        }

        private WFR _PreparePackaging(WF_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- PREPARE PACKING: COPY ADDITIONAL DATA");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

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

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                throw ex;
            }
        }
    }
}
