using System;
using System.IO;
using System.Linq;
using radish.core;
using radish.core.Commands;
using w3tools.common;

namespace w3tools.Workflows
{
    /// <summary>
    /// ENCODING WORLD
    /// </summary>
    [Serializable]
    public class Encode_world : RAD_wf_Command
    {
        public bool SKIP_WORLD_GENERATION { get; set; }
        public bool SKIP_FOLIAGE_GENERATION { get; set; }

        public Encode_world()
        {
            Name = "Encode World";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_WORLD)
                return (int)WFR.WFR_NotRun;

            return _Encode_world(settings);
        }

        private WFR _Encode_world(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING WORLD pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_WORLD()))
                    Directory.CreateDirectory(settings.DIR_OUTPUT_WORLD());
                if (!Directory.Exists(settings.DIR_TMP()))
                    Directory.CreateDirectory(settings.DIR_TMP());

                string sp = $"{settings.WORLD_DEF_PREFIX}*.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_WORLD(), sp, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];
                    settings.LOGGER.LogString($"encoding: {filename}...");

                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w3world() {
                       RepoDirectory = settings.dir_repo_worlds(),
                       OutputDirectory = settings.DIR_OUTPUT_WORLD(),
                       FoliageDirectory = settings.DIR_DEF_WORLD(),
                       SkipFoliageGeneration = SKIP_FOLIAGE_GENERATION,
                       SkipWorldGeneration = SKIP_WORLD_GENERATION,
                       Encode = filename,
                       LogLevel=settings.LOG_LEVEL
                    };
                    WFR result = th.RunCommandSync(encoder);
                    if (result == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }

                //set dependencies
                if (!settings.PATCH_MODE)
                {
                    settings.WCC_ANALYZE = true;
                    settings.WCC_ANALYZE_WORLD = true;
                    settings.WCC_COOK = true;
                    settings.WCC_NAVDATA = true;
                    settings.WCC_OCCLUSIONDATA = true;
                    settings.WCC_TEXTURECACHE = true;
                    settings.WCC_COLLISIONCACHE = true;
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
    /// ENCODING ENVS
    /// </summary>
    [Serializable]
    public class Encode_env : RAD_wf_Command
    {
        public string ENVID { get; set; }
        public Encode_env()
        {
            Name = "Encode Envs";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_ENVS)
                return (int)WFR.WFR_NotRun;

            return _Encode_env(settings);
        }

        private WFR _Encode_env(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING ENVS pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_ENVS()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_ENVS());
                }

                string WILDCARD = $"{settings.ENV_DEF_PREFIX}*.yml";
                if (!String.IsNullOrEmpty(ENVID))
                    WILDCARD = $"{settings.ENV_DEF_PREFIX}{ENVID}.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_ENVS(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];
                    settings.LOGGER.LogString($"encoding: {filename}...");

                    //call encode
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w3env() {
                        OutputDirectory = settings.DIR_OUTPUT_ENVS(),
                        Encode = filename,
                        LogLevel = settings.LOG_LEVEL
                    };
                    WFR result = th.RunCommandSync(encoder);
                    if (result == WFR.WFR_Error)
                        return WFR.WFR_Error;
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
    /// ENCODING SCENES
    /// </summary>
    /// //FIXME
    [Serializable]
    public class Encode_scene : RAD_wf_Command
    {
        public string SCENEID { get; set; }

        public Encode_scene()
        {
            Name = "Encode Scenes";
        }
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_SCENES)
                return (int)WFR.WFR_NotRun;

            return _Encode_scene(settings);
        }

        private WFR _Encode_scene(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING SCENES pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_SCENES()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_SCENES());
                }

                string WILDCARD = $"{settings.SCENE_DEF_PREFIX}*.yml";
                if (!String.IsNullOrEmpty(SCENEID))
                    WILDCARD = $"{settings.SCENE_DEF_PREFIX}{SCENEID}.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_SCENES(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string file = files[i];
                    string SCENENAME = Path.GetFileNameWithoutExtension(file);
                    string SCENEID = SCENENAME.Split(new string[] { settings.SCENE_DEF_PREFIX }, StringSplitOptions.None).Last();
                    settings.LOGGER.LogString($"encoding scene: {SCENEID}...");

                    //FIXME paths
                    //call encoder
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w2scene() {
                        RepoDirectory = settings.dir_repo_scenes(),
                        OutputDirectory = settings.DIR_OUTPUT_SCENES(),
                        Encode = SCENENAME,
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(encoder) == WFR.WFR_Error)
                        return WFR.WFR_Error;

                    //rename scene.<sceneid>.w2scene to <sceneid>.w2scene
                    settings.LOGGER.LogString($"renaming scene to {SCENEID}.w2scene.");
                    string GENERATED_SCENE_FILE = Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENENAME}.w2scene");
                    File.Move(GENERATED_SCENE_FILE, Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENEID}.w2scene"));

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_SCENES(),$"{SCENENAME}.w3strings-csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(),$"{settings.STRINGS_PART_PREFIX}scene-{SCENEID}.csv");
                    if (File.Exists(GENERATED_STRINGS_CSV))
                        File.Move(GENERATED_STRINGS_CSV, STRINGS_PART_CSV);
                }

                //set dependencies
                if (!settings.PATCH_MODE)
                {
                    settings.ENCODE_STRINGS = true;
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
    /// ENCODING QUEST 
    /// FIXME
    /// </summary>
    [Serializable]
    public class Encode_quest : RAD_wf_Command
    {
        public Encode_quest()
        {
            Name = "Encode Quests";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_QUEST)
                return (int)WFR.WFR_NotRun;

            return _Encode_quest(settings);
        }

        private WFR _Encode_quest(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING QUESTS pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_QUEST()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_QUEST());
                }

                string WILDCARD = $"*.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_QUEST(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];
                    settings.LOGGER.LogString($"encoding quest: {filename}...");

                    //call encoder
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w2quest() {
                        RepoDirectory = settings.dir_repo_quests(),
                        OutputDirectory = settings.DIR_OUTPUT_QUEST(),
                        Encode = filename,
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(encoder) == WFR.WFR_Error)
                        return WFR.WFR_Error;

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_QUEST(), "queststrings.csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(), $"{settings.STRINGS_PART_PREFIX}quest.csv");
                    if (File.Exists(GENERATED_STRINGS_CSV))
                        File.Move(GENERATED_STRINGS_CSV, STRINGS_PART_CSV);

                }
                //set dependencies
                if (!settings.PATCH_MODE)
                {
                    settings.ENCODE_STRINGS = true;
                    settings.WCC_ANALYZE = true;
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
    /// ENCODING STRINGS
    /// </summary>
    [Serializable]
    public class Encode_strings : RAD_wf_Command
    {
        public Encode_strings()
        {
            Name = "Encode Strings";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_STRINGS)
                return (int)WFR.WFR_NotRun;

            return _Encode_strings(settings);
        }

        private WFR _Encode_strings(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING STRINGS pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                //if (!Directory.Exists(settings.DIR_OUTPUT_STRINGS()))
                //    Directory.CreateDirectory(settings.DIR_OUTPUT_STRINGS());
                if (!Directory.Exists(settings.DIR_DLC_CONTENT()))
                    Directory.CreateDirectory(settings.DIR_DLC_CONTENT());

                // collect snippets into one csv file
                settings.LOGGER.LogString($"merging strings.csv parts...");
                string STRINGS_FILE_COMBINED = Path.Combine(settings.DIR_STRINGS(), "all.en.strings.csv");
                string W3_STRINGS_FILE = $"{STRINGS_FILE_COMBINED}.w3strings";

                if (File.Exists(STRINGS_FILE_COMBINED))
                    File.Delete(STRINGS_FILE_COMBINED);

                string sp = $"{settings.STRINGS_PART_PREFIX}*.csv";
                var files = Directory.GetFiles(settings.DIR_STRINGS(), sp, SearchOption.AllDirectories);

                using (Stream destStream = File.OpenWrite(STRINGS_FILE_COMBINED))
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filename = files[i];
                        settings.LOGGER.LogString($"merging: {filename}");

                        using (Stream srcStream = File.OpenRead(filename))
                        {
                            srcStream.CopyTo(destStream);
                        }
                    } 
                }

                // --- encode csv to w3strings
                if (File.Exists(STRINGS_FILE_COMBINED))
                {
                    settings.LOGGER.LogString($"encoding to w3strings...");

                    //call encoder
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w3strings() {
                        Encode = STRINGS_FILE_COMBINED,
                        IdSpace = settings.idspace,
                        AutoGenerateMissingIds = true,
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(encoder) == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }
                // COPYING W3STRINGS INTO DLC FOLDER
                settings.LOGGER.LogString($"COPYING W3STRINGS INTO DLC FOLDER");
                if (File.Exists(W3_STRINGS_FILE))
                    File.Copy(W3_STRINGS_FILE,Path.Combine(settings.DIR_DLC_CONTENT(), "en.w3strings"));

                // cleanup
                if (File.Exists(W3_STRINGS_FILE))
                    File.Delete(W3_STRINGS_FILE);
                if (File.Exists($"{W3_STRINGS_FILE}.ws"))
                    File.Delete($"{W3_STRINGS_FILE}.ws");

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
    /// ENCODING SPEECH
    /// </summary>
    [Serializable]
    public class Encode_speech : RAD_wf_Command
    {
        public Encode_speech()
        {
            Name = "Encode Speech";
        }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() == WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_SPEECH)
                return (int)WFR.WFR_NotRun;

            return _Encode_speech(settings);
        }

        private WFR _Encode_speech(RAD_Settings settings)
        {
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");
            settings.LOGGER.LogString($"-- ENCODING SPEECH pm: {settings.PATCH_MODE}");
            settings.LOGGER.LogString($"--------------------------------------------------------------------------");

            try
            {
                // GENERATING LIPSYNC ANIMATIONS
                if (Directory.Exists(settings.DIR_PHONEMES()))
                {
                    settings.LOGGER.LogString($"GENERATING LIPSYNC ANIMATIONS");

                    string file = Directory.GetFiles(settings.DIR_PHONEMES(), "*.phonemes", SearchOption.AllDirectories).First();

                    //call encoder
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w3speech_lipsync_creator() {
                        CreateLipsync = file,
                        OutputDirectory = settings.DIR_AUDIO_WEM(),
                        RepoDirectory = settings.dir_repo_lipsync(),
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(encoder) == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }

                // ENCODING LIPSYNC ANIMATIONS TO CR2W
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    settings.LOGGER.LogString($"ENCODING LIPSYNC ANIMATIONS TO CR2W");

                    //call encoder
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var encoder = new w3speech() {
                        Encode = settings.DIR_AUDIO_WEM(),
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(encoder) == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }

                // CREATING W3SPEECH FILE
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    settings.LOGGER.LogString($"CREATING W3SPEECH FILE");

                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER, settings.LOGGER);
                    var pack = new w3speech() {
                        Pack = settings.DIR_AUDIO_WEM(),
                        OutputDirectory = settings.DIR_DLC_CONTENT(),
                        Language = settings.language,
                        LogLevel = settings.LOG_LEVEL
                    };
                    if (th.RunCommandSync(pack) == WFR.WFR_Error)
                        return WFR.WFR_Error;
                }

                // UPDATING DLC W3SPEECH FILE
                settings.LOGGER.LogString($"UPDATING DLC W3SPEECH FILE");


                string speech_packed_file = Path.Combine(settings.DIR_DLC_CONTENT(), $"speech.{settings.language}.w3speech.packed");
                string speech_final_file = Path.Combine(settings.DIR_DLC_CONTENT(), $"{settings.language}pc.w3speech");

                if (File.Exists(speech_final_file))
                    File.Delete(speech_final_file);
                File.Move(speech_packed_file, speech_final_file);

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

    

}
