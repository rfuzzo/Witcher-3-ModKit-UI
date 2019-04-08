using System;
using System.IO;
using System.Linq;
using radish.core;
using radish.core.Commands;
using w3.workflow;
using w3.tools;
using w3.tools.Commands;

namespace w3.tools.Commands
{
    /// <summary>
    /// ENCODING WORLD
    /// </summary>
    [Serializable]
    public class Encode_world : RAD_wf_Command
    {
        public bool SKIP_WORLD_GENERATION { get; set; }
        public bool SKIP_FOLIAGE_GENERATION { get; set; }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_WORLD)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_world(settings);
            return result;
        }

        private WFR _Encode_world(RAD_Settings settings)
        {
            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_WORLD()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_WORLD());
                }
                if (!Directory.Exists(settings.DIR_TMP()))
                {
                    Directory.CreateDirectory(settings.DIR_TMP());
                }

                string WILDCARD = $"{settings.WORLD_DEF_PREFIX}*.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_WORLD(), WILDCARD, SearchOption.AllDirectories);

                string SKIP_PARAM = "";
                if (SKIP_WORLD_GENERATION)
                    SKIP_PARAM = "--no-terrain";
                if (SKIP_FOLIAGE_GENERATION)
                    SKIP_PARAM += "--no-foliage";

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];

                    string arg = $"--repo-dir {settings.dir_repo_worlds()} " +
                        $"--output-dir {settings.DIR_OUTPUT_WORLD()}  " +
                        $"--foliage-dir {settings.DIR_DEF_WORLD()} " +
                        $"{SKIP_PARAM} " +
                        $"--encode {filename} " +
                        $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3world() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
                }

                //set dependencies
                if (settings.PATCH_MODE)
                {

                }
                else
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
    /// ENCODING ENVS
    /// </summary>
    [Serializable]
    public class Encode_env : RAD_wf_Command
    {
        public string ENVID { get; set; }

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_ENVS)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_env(settings);
            return result;
        }

        private WFR _Encode_env(RAD_Settings settings)
        {
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
                    string arg = $"--output-dir {settings.DIR_OUTPUT_ENVS()}  " +
                        $"--encode {filename} " +
                        $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3env() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
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
    /// ENCODING SCENES
    /// </summary>
    [Serializable]
    public class Encode_scene : RAD_wf_Command
    {
        public string SCENEID { get; set; }
        

        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_SCENES)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_scene(settings);
            return result;
        }

        private WFR _Encode_scene(RAD_Settings settings)
        {
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
                    string filename = files[i];
                    SCENEID = $"{filename}:{settings.SCENE_DEF_PREFIX}="; //FIXME
                    string SCENENAME = filename;
                    //PUSHD %DIR_DEF_SCENES%

                    string arg = $"--repo-dir {settings.dir_repo_scenes()}" + 
                        $"--output-dir {settings.DIR_OUTPUT_SCENES()}  " +
                        $"--encode {SCENENAME} " +
                        $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w2scene() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);

                    //rename scene.<sceneid>.w2scene to <sceneid>.w2scene
                    string GENERATED_SCENE_FILE = Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENENAME}.w2scene");
                    File.Move(GENERATED_SCENE_FILE, Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENEID}.w2scene"));

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_SCENES(),$"{SCENENAME}.w3strings-csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(),$"{settings.STRINGS_PART_PREFIX}scene-{SCENEID}.csv");
                    if (File.Exists(GENERATED_STRINGS_CSV))
                        File.Move(GENERATED_STRINGS_CSV, STRINGS_PART_CSV);
                }

                //dependencies
                   /*SET ENCODE_STRINGS = 1
                   SET WCC_COOK = 1
                   SET WCC_REPACK_DLC = 1*/
         
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
    /// ENCODING QUEST 
    /// FIXME
    /// </summary>
    [Serializable]
    public class Encode_quest : RAD_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_QUEST)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_quest(settings);
            return result;
        }

        private WFR _Encode_quest(RAD_Settings settings)
        {
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
                    string arg = $"--repo-dir {settings.dir_repo_quests()}" +
                            $"--output-dir {settings.DIR_OUTPUT_QUEST()}  " +
                            $"--encode {filename} " +
                            $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w2quest() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_QUEST(), "queststrings.csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(), $"{settings.STRINGS_PART_PREFIX}quest.csv");
                    if (File.Exists(GENERATED_STRINGS_CSV))
                        File.Move(GENERATED_STRINGS_CSV, STRINGS_PART_CSV);

                }
                //dependencies
                /*
                 * SET ENCODE_STRINGS=1
                    SET WCC_ANALYZE=1
                    SET WCC_COOK=1
                    SET WCC_REPACK_DLC=1
                    */

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
    /// ENCODING STRINGS
    /// </summary>
    [Serializable]
    public class Encode_strings : RAD_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_STRINGS)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_strings(settings);
            return result;
        }

        private WFR _Encode_strings(RAD_Settings settings)
        {
            try
            {
                //if (!Directory.Exists(settings.DIR_OUTPUT_STRINGS()))
                //    Directory.CreateDirectory(settings.DIR_OUTPUT_STRINGS());
                if (!Directory.Exists(settings.DIR_DLC_CONTENT()))
                    Directory.CreateDirectory(settings.DIR_DLC_CONTENT());

                // collect snippets into one csv file

                string STRINGS_FILE_COMBINED = Path.Combine(settings.DIR_STRINGS(), "all.en.strings.csv");
                string W3_STRINGS_FILE = $"{STRINGS_FILE_COMBINED}.w3strings";

                if (File.Exists(STRINGS_FILE_COMBINED))
                    File.Delete(STRINGS_FILE_COMBINED);

                string WILDCARD = $"{settings.STRINGS_PART_PREFIX}*.csv";
                var files = Directory.GetFiles(settings.DIR_STRINGS(), WILDCARD, SearchOption.AllDirectories);

                using (Stream destStream = File.OpenWrite(STRINGS_FILE_COMBINED))
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filename = files[i];
                        using (Stream srcStream = File.OpenRead(filename))
                        {
                            srcStream.CopyTo(destStream);
                        }
                    } 
                }

                // --- encode csv to w3strings
                if (File.Exists(STRINGS_FILE_COMBINED))
                {
                    string arg = $"--encode {STRINGS_FILE_COMBINED} " +
                         $"--id-space {settings.idspace}" + 
                         $"--auto-generate-missing-ids" +
                        $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3strings() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
                }
                // COPYING W3STRINGS INTO DLC FOLDER
                if (File.Exists(W3_STRINGS_FILE))
                {
                    File.Copy(W3_STRINGS_FILE,Path.Combine(settings.DIR_DLC_CONTENT(), "en.w3strings"));
                }

                // cleanup
                if (File.Exists(W3_STRINGS_FILE))
                    File.Delete(W3_STRINGS_FILE);
                if (File.Exists($"{W3_STRINGS_FILE}.ws"))
                    File.Delete($"{W3_STRINGS_FILE}.ws");

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
    /// ENCODING SPEECH
    /// </summary>
    [Serializable]
    public class Encode_speech : RAD_wf_Command
    {
        public override WFR Run()
        {
            // check if any higher level detects any error
            if (base.Run() != WFR.WFR_Error)
                return WFR.WFR_Error;
            // check if step is disabled
            RAD_Settings settings = (RAD_Settings)base.Parent;
            if (!settings.ENCODE_SPEECH)
                return (int)WFR.WFR_NotRun;

            WFR result = _Encode_speech(settings);
            return result;
        }

        private WFR _Encode_speech(RAD_Settings settings)
        {
            try
            {
                // GENERATING LIPSYNC ANIMATIONS
                if (Directory.Exists(settings.DIR_PHONEMES()))
                {
                    string file = Directory.GetFiles(settings.DIR_PHONEMES(), "*.phonemes", SearchOption.AllDirectories).First();

                    string arg = $"--create-lipsync {file} " +
                                $"--output-dir {settings.DIR_AUDIO_WEM()}" +
                                $"--repo-dir {settings.dir_repo_lipsync()}" +
                               $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech_lipsync_creator() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
                }

                // ENCODING LIPSYNC ANIMATIONS TO CR2W
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    string arg = $"--encode-cr2w {settings.DIR_AUDIO_WEM()} " +
                              $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
                }

                // CREATING W3SPEECH FILE
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    string arg = $"--pack-w3speech {settings.DIR_AUDIO_WEM()} " +
                                $"--output-dir {settings.DIR_DLC_CONTENT()}" +
                                $"--language {settings.language}" +
                              $"{settings.EnumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech() { Args = arg };
                    th.RunArgsSync(encoder.ToString(), encoder.Args);
                }

                // UPDATING DLC W3SPEECH FILE
                string speech_packed_file = Path.Combine(settings.DIR_DLC_CONTENT(), $"speech.{settings.language}.w3speech.packed");
                string speech_final_file = Path.Combine(settings.DIR_DLC_CONTENT(), $"{settings.language}pc.w3speech");

                if (File.Exists(speech_final_file))
                    File.Delete(speech_final_file);
                File.Move(speech_packed_file, speech_final_file);

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
