using System;
using System.IO;
using System.Linq;
using radish.core;
using radish.core.Commands;
using w3.workflow;
using w3.tools;
using w3.tools.Commands;
using wcc.core;
using wcc.core.Commands;

namespace w3.tools.Commands
{
    /// <summary>
    /// WCC_LITE: IMPORT MODELS
    /// </summary>
    [Serializable]
    public class WCC_import : WCC_wf_Command
    {
        public string MODELNAME { get; set; }
        private const string MODEL_PREFIX = "model";

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_IMPORT_MODELS)
                return (int)WFR.WFR_NotRun;

            WFR result = _WCC_import(settings);
            return result;
        }

        private WFR _WCC_import(RAD_Settings settings)
        {
            try
            {
                //get all files with specified filter
                string WILDCARD = $"{MODEL_PREFIX}*.fbx";
                if (!String.IsNullOrEmpty(MODELNAME))
                    WILDCARD = $"{MODEL_PREFIX}{MODELNAME}.fbx";
                var files = Directory.GetFiles(settings.DIR_MODEL_FBX(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    //LOG WCC_LITE: IMPORT MODEL FOR !MODELNAME!

                    string filename = files[i]; //path
                    MODELNAME = Path.GetFileNameWithoutExtension(filename);
                    MODELNAME = MODELNAME.Split(new string[] { MODEL_PREFIX }, StringSplitOptions.None).Last();

                    //call wcc_lite
                    WCC_Task th = new WCC_Task(settings.DIR_MODKIT);
                    WccCommand cmd = new import() {
                        Depot = $"{settings.DIR_MODKIT_DEPOT()}",
                        File = filename,
                        Out = Path.Combine(settings.DIR_OUTPUT_MESHES(),$"{MODELNAME}.w2mesh"),
                    };
                    th.RunCommandSync(cmd);
                }

                //set dependencies
                if (settings.PATCH_MODE)
                {

                }
                else
                {
                    string sp = $"{settings.WORLD_DEF_PREFIX}*.yml";
                    if (Directory.GetFiles(settings.DIR_DEF_WORLD(),sp, SearchOption.AllDirectories).Any())
                    {
                        settings.WCC_NAVDATA = true;
                    }
                    settings.WCC_ANALYZE = true;
                    settings.WCC_COLLISIONCACHE = true;
                    settings.WCC_COOK = true;
                    settings.WCC_REPACK_DLC = true;
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
    /// WCC_LITE: GENERATE OCCLUSIONDATA
    /// </summary>
    [Serializable]
    public class WCC_occlusion : WCC_wf_Command
    {

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_OCCLUSIONDATA)
                return (int)WFR.WFR_NotRun;

            WFR result = _WCC_occlusion(settings);
            return result;
        }

        private WFR _WCC_occlusion(RAD_Settings settings)
        {
            try
            {
                //get all folders inside the levels directory
                string levelDir = Path.Combine(settings.DIR_UNCOOKED(), settings.DIR_DLC_GAMEPATH(), "levels");
                var levels = Directory.GetDirectories(levelDir);

                for (int i = 0; i < levels.Length; i++)
                {
                    string level = levels[i]; //path
                    string worldname = Path.GetDirectoryName(level);

                    //check i w2w file in subfolder?
                    string worldfile = Path.Combine(levelDir, worldname, $"{worldname}.w2w");
                    if (File.Exists(worldfile))
                    {
                        //LOG WCC_LITE: GENERATE OCCLUSIONDATA FOR !WORLDNAME!

                        //call wcc_lite
                        WCC_Task th = new WCC_Task(settings.DIR_MODKIT);
                        WccCommand cmd = new cookocclusion()
                        {
                            WorldFile = worldfile
                        };
                        th.RunCommandSync(cmd);
                    }
                }

                //set dependencies
                if (settings.PATCH_MODE)
                {

                }
                else
                {
                   
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
    /// WCC_LITE: GENERATE OCCLUSIONDATA
    /// </summary>
    [Serializable]
    public class WCC_analyze : WCC_wf_Command
    {

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_ANALYZE)
                return (int)WFR.WFR_NotRun;

            WFR result = _WCC_analyze(settings);
            return result;
        }

        private WFR _WCC_analyze(RAD_Settings settings)
        {
            try
            {
                if (!Directory.Exists(settings.DIR_TMP()))
                    Directory.CreateDirectory(settings.DIR_TMP());

                // process all worlds IF any world was generated
                if (settings.ENCODE_WORLD || settings.WCC_ANALYZE_WORLD)
                {
                    //get all folders inside the levels directory
                    string levelDir = Path.Combine(settings.DIR_UNCOOKED(), settings.DIR_DLC_GAMEPATH(), "levels");
                    var levels = Directory.GetDirectories(levelDir);

                    for (int i = 0; i < levels.Length; i++)
                    {
                        string level = levels[i]; //path
                        string worldname = Path.GetDirectoryName(level);

                        //check i w2w file in subfolder?
                        string worldfile = Path.Combine(levelDir, worldname, $"{worldname}.w2w");
                        if (File.Exists(worldfile))
                        {
                            //LOG WCC_LITE: ANALYZE WORLD FOR !WORLDNAME!

                            //call wcc_lite
                            WCC_Task th = new WCC_Task(settings.DIR_MODKIT);
                            WccCommand cmd = new analyze()
                            {
                                Analyzer = analyzers.world,
                                Object = Path.Combine(settings.DIR_DLC_GAMEPATH(), "levels", worldname, $"{worldname}.w2w"),
                                Out = Path.Combine(settings.DIR_TMP(), $"{settings.SEEDFILE_PREFIX}world.{worldname}.files")
                            };
                            th.RunCommandSync(cmd);
                        }
                    }
                }
                else //process dlc IF something changed
                {
                    if (settings.ENCODE_QUEST || settings.WCC_IMPORT_MODELS)
                    {
                        //LOG WCC_LITE: ANALYZE DLC
                        //call wcc_lite
                        WCC_Task th = new WCC_Task(settings.DIR_MODKIT);
                        WccCommand cmd = new analyze()
                        {
                            Analyzer = analyzers.r4dlc,
                            reddlc = Path.Combine(settings.DIR_DLC_GAMEPATH(), $"dlc{settings.MODNAME}.reddlc"),
                            Out = Path.Combine(settings.DIR_TMP(), $"{settings.SEEDFILE_PREFIX}dlc{settings.MODNAME}.files")
                        };
                        th.RunCommandSync(cmd);
                    }
                }


                //set dependencies
                if (settings.PATCH_MODE)
                {

                }
                else
                {

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

}
