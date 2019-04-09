using System;
using System.IO;
using System.Linq;
using radish.core;
using radish.core.Commands;
using w3tools.common;
using w3tools;
using w3tools.Commands;
using wcc.core;
using wcc.core.Commands;

namespace w3tools.Commands
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
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_IMPORT_MODELS)
                return (int)WFR.WFR_NotRun;

            return _WCC_import(settings);
        }

        private WFR _WCC_import(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: IMPORTING MODELS pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                //get all files with specified filter
                string WILDCARD = $"{MODEL_PREFIX}*.fbx";
                if (!String.IsNullOrEmpty(MODELNAME))
                    WILDCARD = $"{MODEL_PREFIX}{MODELNAME}.fbx";
                var files = Directory.GetFiles(settings.DIR_MODEL_FBX(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i]; //path
                    MODELNAME = Path.GetFileNameWithoutExtension(filename);
                    MODELNAME = MODELNAME.Split(new string[] { MODEL_PREFIX }, StringSplitOptions.None).Last();
                    settings.LOGGER.LogString($"importing: {MODELNAME}...");

                    //call wcc_lite
                    WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                    WccCommand cmd = new import() {
                        Depot = $"{settings.DIR_MODKIT_DEPOT()}",
                        File = filename,
                        Out = Path.Combine(settings.DIR_OUTPUT_MESHES(),$"{MODELNAME}.w2mesh"),
                    };
                    WFR result = th.RunCommandSync(cmd);
                    if (result == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }

                //set dependencies
                if (!settings.PATCH_MODE)
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

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
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
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_OCCLUSIONDATA)
                return (int)WFR.WFR_NotRun;

            return _WCC_occlusion(settings);
        }

        private WFR _WCC_occlusion(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: COOK OCCLUSION pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                //get all folders inside the levels directory
                string levelDir = Path.Combine(settings.DIR_UNCOOKED(), settings.DIR_DLC_GAMEPATH(), "levels");
                var levels = Directory.GetDirectories(levelDir);

                for (int i = 0; i < levels.Length; i++)
                {
                    string level = levels[i]; //path
                    string worldname = Path.GetDirectoryName(level);
                    settings.LOGGER.LogString($"generate occlusiondata for: {worldname}...");

                    //check i w2w file in subfolder?
                    string worldfile = Path.Combine(levelDir, worldname, $"{worldname}.w2w");
                    if (File.Exists(worldfile))
                    {
                        //LOG WCC_LITE: GENERATE OCCLUSIONDATA FOR !WORLDNAME!

                        //call wcc_lite
                        WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                        WccCommand cmd = new cookocclusion()
                        {
                            WorldFile = worldfile
                        };
                        WFR result = th.RunCommandSync(cmd);
                        if (result == WFR.WFR_Error)
                            return WFR.WFR_Error;
                    }
                }

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: ANALYZE files
    /// </summary>
    [Serializable]
    public class WCC_analyze : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_ANALYZE)
                return (int)WFR.WFR_NotRun;

            return _WCC_analyze(settings);        }

        private WFR _WCC_analyze(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: ANALYZE FILES pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

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
                        settings.LOGGER.LogString($"analyze world for: {worldname}...");

                        //check i w2w file in subfolder?
                        string worldfile = Path.Combine(levelDir, worldname, $"{worldname}.w2w");
                        if (File.Exists(worldfile))
                        {
                            //LOG WCC_LITE: ANALYZE WORLD FOR !WORLDNAME!

                            //call wcc_lite
                            WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                            WccCommand cmd = new analyze()
                            {
                                Analyzer = analyzers.world,
                                Object = Path.Combine(settings.DIR_DLC_GAMEPATH(), "levels", worldname, $"{worldname}.w2w"),
                                Out = Path.Combine(settings.DIR_TMP(), $"{settings.SEEDFILE_PREFIX}world.{worldname}.files")
                            };
                            WFR result = th.RunCommandSync(cmd);

                            //if any wcc operation fails return
                            if (result == WFR.WFR_Error)
                                return WFR.WFR_Error;
                        }
                    }
                }
                else //process dlc IF something changed
                {
                    if (settings.ENCODE_QUEST || settings.WCC_IMPORT_MODELS)
                    {
                        //LOG WCC_LITE: ANALYZE DLC
                        //call wcc_lite
                        WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                        WccCommand cmd = new analyze()
                        {
                            Analyzer = analyzers.r4dlc,
                            reddlc = Path.Combine(settings.DIR_DLC_GAMEPATH(), $"dlc{settings.MODNAME}.reddlc"),
                            Out = Path.Combine(settings.DIR_TMP(), $"{settings.SEEDFILE_PREFIX}dlc{settings.MODNAME}.files")
                        };
                        return th.RunCommandSync(cmd);
                    }
                }

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: GENERATE NAVDATA
    /// </summary>
    [Serializable]
    public class WCC_GenerateNavData : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_NAVDATA)
                return (int)WFR.WFR_NotRun;

            return _WCC_GenerateNavData(settings);        }

        private WFR _WCC_GenerateNavData(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: GENERATE NAVDATA pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                WccCommand cmd = new pathlib()
                {
                    RootSearchDir = settings.DIR_WCC_DEPOT_WORLDS(),
                    FilePattern = "*.w2w"
                };

                settings.LOGGER.LogString("done.");
                return th.RunCommandSync(cmd);
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: COOK
    /// </summary>
    [Serializable]
    public class WCC_CookData : WCC_wf_Command
    {
        private string WCC_SEEDFILES;

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_COOK)
                return (int)WFR.WFR_NotRun;

            return _WCC_CookData(settings);
        }

        private WFR _WCC_CookData(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: COOK pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                WFR result = WFR.WFR_Error;

                // setup *all* seedfiles for cooking: hubs, dlc
                string sp = $"{settings.SEEDFILE_PREFIX}*.files";
                var seedfiles = Directory.GetFiles(settings.DIR_TMP(), sp, SearchOption.TopDirectoryOnly);
                foreach (var file in seedfiles)
                {
                    WCC_SEEDFILES += $"-seed={file}";
                }
                // Note: trimdir MUST be lowercased!

                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                

                WccCommand cmd = new cook()
                {
                    Platform = platform.pc,
                    trimdir = Path.Combine("dlc", $"dlc{settings.MODNAME_LC()}"),
                    outdir = settings.DIR_COOKED_DLC()
                };

                // run as arguments because of multiple seedfiles
                string args = cmd.CommandLine;
                args += $" {WCC_SEEDFILES}";
                result = th.RunArgsSync(args, cmd.Name);

                //cleanup
                if (Directory.Exists(settings.DIR_COOKED_FILES_DB()))
                    Directory.Delete(settings.DIR_COOKED_FILES_DB());
                //move to prevent beeing packed into mod
                if (File.Exists(Path.Combine(settings.DIR_COOKED_DLC(), "cook.db")))
                    File.Move(Path.Combine(settings.DIR_COOKED_DLC(), "cook.db"), Path.Combine(settings.DIR_COOKED_FILES_DB(), "cook.db"));

                settings.LOGGER.LogString("done.");
                return result;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: PACK + METADATASTORE DLC
    /// </summary>
    [Serializable]
    public class WCC_PackDLCAndCreateMetadatastore : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_REPACK_DLC)
                return (int)WFR.WFR_NotRun;

            return _WCC_PackDLCAndCreateMetadatastore(settings);
        }

        private WFR _WCC_PackDLCAndCreateMetadatastore(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: PACK + METADATASTORE DLC pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                WFR result_pack = WFR.WFR_Error;
                WFR result_meta = WFR.WFR_Error;

                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                WccCommand pack = new pack()
                {
                    Directory=settings.DIR_COOKED_DLC(),
                    Outdir=settings.DIR_DLC_CONTENT()
                };
                result_pack = th.RunCommandSync(pack);
                if (result_pack == WFR.WFR_Error)
                    return WFR.WFR_Error;

                WccCommand metadata = new metadatastore()
                {
                    Directory= settings.DIR_DLC_CONTENT()
                };
                result_meta = th.RunCommandSync(metadata);
                if (result_meta == WFR.WFR_Error)
                    return WFR.WFR_Error;

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: PACK + METADATASTORE MOD
    /// </summary>
    [Serializable]
    public class WCC_PackMODAndCreateMetadatastore : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_REPACK_MOD)
                return (int)WFR.WFR_NotRun;

            return _WCC_PackMODAndCreateMetadatastore(settings);
        }

        private WFR _WCC_PackMODAndCreateMetadatastore(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: PACK + METADATASTORE MOD pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                WFR result_pack = WFR.WFR_Error;
                WFR result_meta = WFR.WFR_Error;

                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                WccCommand pack = new pack()
                {
                    Directory = settings.DIR_COOKED_MOD(),
                    Outdir = settings.DIR_MOD_CONTENT()
                };
                result_pack = th.RunCommandSync(pack);
                if (result_pack == WFR.WFR_Error)
                    return WFR.WFR_Error;

                WccCommand metadata = new metadatastore()
                {
                    Directory = settings.DIR_MOD_CONTENT()
                };
                result_meta = th.RunCommandSync(metadata);
                if (result_meta == WFR.WFR_Error)
                    return WFR.WFR_Error;

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: GENERATE TEXTURE CACHE
    /// </summary>
    [Serializable]
    public class WCC_GenerateTextureCache : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_TEXTURECACHE)
                return (int)WFR.WFR_NotRun;

            return _WCC_GenerateTextureCache(settings);
        }

        private WFR _WCC_GenerateTextureCache(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: GENERATE TEXTURE CACHE pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            if (!Directory.Exists(Path.Combine(settings.DIR_UNCOOKED_TEXTURES(), settings.DIR_DLC_GAMEPATH())))
            {
                settings.LOGGER.LogString($"WARNING: no textures found in {Path.Combine(settings.DIR_UNCOOKED_TEXTURES(), settings.DIR_DLC_GAMEPATH())}");
                return WFR.WFR_Error;
            }

            try
            {
                WFR result_cook = WFR.WFR_Error;
                WFR result_cache = WFR.WFR_Error;

                if (Directory.Exists(settings.DIR_COOKED_TEXTURES_DB()))
                    Directory.Delete(settings.DIR_COOKED_TEXTURES_DB());
    
                // cook textures
                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                WccCommand cook = new cook()
                {
                    Platform=platform.pc,
                    mod=settings.DIR_UNCOOKED_TEXTURES(),
                    basedir=settings.DIR_UNCOOKED_TEXTURES(),
                    outdir=settings.DIR_COOKED_DLC()
                };
                result_cook = th.RunCommandSync(cook);
                if (result_cook == WFR.WFR_Error)
                    return WFR.WFR_Error;

                // move so it is separated from "normal" files cook.db
                if (File.Exists(Path.Combine(settings.DIR_COOKED_DLC(), "cook.db")))
                    File.Delete(Path.Combine(settings.DIR_COOKED_DLC(), "cook.db"));

                // build texture cache
                WccCommand buildcache = new buildcache()
                {
                    builder=cachebuilder.textures,
                    DataBase= settings.DIR_COOKED_TEXTURES_DB(),
                    basedir= settings.DIR_UNCOOKED_TEXTURES(),
                    Out= Path.Combine(settings.DIR_DLC_CONTENT(),"texture.cache"),
                    Platform=platform.pc
                };
                result_cache = th.RunCommandSync(buildcache);
                if (result_cache == WFR.WFR_Error)
                    return WFR.WFR_Error;

                settings.LOGGER.LogString("done.");
                return WFR.WFR_Finished;
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }

    /// <summary>
    /// WCC_LITE: GENERATE COLLISION CACHE
    /// </summary>
    [Serializable]
    public class WCC_GenerateCollisionCache : WCC_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.WCC_COLLISIONCACHE)
                return (int)WFR.WFR_NotRun;

            return _WCC_GenerateCollisionCache(settings);
        }

        private WFR _WCC_GenerateCollisionCache(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- WCC_LITE: GENERATE COLLISION CACHE pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                WCC_Task th = new WCC_Task(settings.DIR_MODKIT, settings.LOGGER);
                WccCommand buildcache = new buildcache()
                {
                    builder = cachebuilder.physics,
                    DataBase = settings.DIR_COOKED_FILES_DB(),
                    basedir = settings.DIR_MODKIT_DEPOT(),
                    Out = Path.Combine(settings.DIR_DLC_CONTENT(), "collision.cache"),
                    Platform = platform.pc
                };

                settings.LOGGER.LogString("done.");
                return th.RunCommandSync(buildcache);
            }
            catch (Exception ex)
            {
                settings.LOGGER.LogString(ex.ToString());
                return WFR.WFR_Error; //FIXME
                throw ex;
            }
        }
    }


}
