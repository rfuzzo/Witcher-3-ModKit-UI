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

        private const string WORLD_DEF_PREFIX = "world";
        public bool SKIP_WORLD_GENERATION { get; set; }
        public bool SKIP_FOLIAGE_GENERATION { get; set; }

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_world();
        }

        private int _Encode_world()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

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

                string WILDCARD = $"{WORLD_DEF_PREFIX}*.yml";
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
                        $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3world() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
                }

                //set dependencies
                //FIXME
                /*
        * SET WCC_ANALYZE=1
       SET WCC_ANALYZE_WORLD=1
       SET WCC_COOK=1
       SET WCC_NAVDATA=1
       SET WCC_OCCLUSIONDATA=1
       SET WCC_TEXTURECACHE=1
       SET WCC_COLLISIONCACHE=1
       SET WCC_REPACK_DLC=1
       */

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
    /// ENCODING ENVS
    /// </summary>
    [Serializable]
    public class Encode_env : RAD_wf_Command
    {
        public string ENVID { get; set; }
        private const string ENV_DEF_PREFIX = "env";

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_env();
        }

        private int _Encode_env()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_ENVS()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_ENVS());
                }

                string WILDCARD = $"{ENV_DEF_PREFIX}*.yml";
                if (!String.IsNullOrEmpty(ENVID))
                    WILDCARD = $"{ENV_DEF_PREFIX}{ENVID}.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_ENVS(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];
                    string arg = $"--output-dir {settings.DIR_OUTPUT_ENVS()}  " +
                        $"--encode {filename} " +
                        $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3env() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
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
    /// ENCODING SCENES
    /// </summary>
    [Serializable]
    public class Encode_scene : RAD_wf_Command
    {
        public string SCENEID { get; set; }
        private const string SCENE_DEF_PREFIX = "scene";
        private const string STRINGS_PART_PREFIX = "strings";

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_scene();
        }

        private int _Encode_scene()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                if (!Directory.Exists(settings.DIR_OUTPUT_SCENES()))
                {
                    Directory.CreateDirectory(settings.DIR_OUTPUT_SCENES());
                }

                string WILDCARD = $"{SCENE_DEF_PREFIX}*.yml";
                if (!String.IsNullOrEmpty(SCENEID))
                    WILDCARD = $"{SCENE_DEF_PREFIX}{SCENEID}.yml";
                var files = Directory.GetFiles(settings.DIR_DEF_SCENES(), WILDCARD, SearchOption.AllDirectories);

                for (int i = 0; i < files.Length; i++)
                {
                    string filename = files[i];
                    SCENEID = $"{filename}:{SCENE_DEF_PREFIX}="; //FIXME
                    string SCENENAME = filename;
                    //PUSHD %DIR_DEF_SCENES%

                    string arg = $"--repo-dir {settings.dir_repo_scenes()}" + 
                        $"--output-dir {settings.DIR_OUTPUT_SCENES()}  " +
                        $"--encode {SCENENAME} " +
                        $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w2scene() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);

                    //rename scene.<sceneid>.w2scene to <sceneid>.w2scene
                    string GENERATED_SCENE_FILE = Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENENAME}.w2scene");
                    File.Move(GENERATED_SCENE_FILE, Path.Combine(settings.DIR_OUTPUT_SCENES(), $"{SCENEID}.w2scene"));

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_SCENES(),$"{SCENENAME}.w3strings-csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(),$"{STRINGS_PART_PREFIX}scene-{SCENEID}.csv");
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
                return -1;
                throw;
            }

            return 1;
        }
    }

    /// <summary>
    /// ENCODING QUEST 
    /// FIXME
    /// </summary>
    [Serializable]
    public class Encode_quest : RAD_wf_Command
    {
        private const string SCENE_DEF_PREFIX = "scene";
        private const string STRINGS_PART_PREFIX = "strings";

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_quest();
        }

        private int _Encode_quest()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

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
                            $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w2quest() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);

                    //put generated strings into strings dir for later concatenation
                    string GENERATED_STRINGS_CSV = Path.Combine(settings.DIR_OUTPUT_QUEST(), "queststrings.csv");
                    string STRINGS_PART_CSV = Path.Combine(settings.DIR_STRINGS(), $"{STRINGS_PART_PREFIX}quest.csv");
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
                return -1;
                throw;
            }

            return 1;
        }
    }

    /// <summary>
    /// ENCODING STRINGS
    /// </summary>
    [Serializable]
    public class Encode_strings : RAD_wf_Command
    {
        private const string SCENE_DEF_PREFIX = "scene";
        private const string STRINGS_PART_PREFIX = "strings";

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_strings();
        }

        private int _Encode_strings()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

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

                string WILDCARD = $"{STRINGS_PART_PREFIX}*.csv";
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
                        $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder //FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3strings() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
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
                return -1;
                throw;
            }

            return 1;
        }
    }

    /// <summary>
    /// ENCODING SPEECH
    /// </summary>
    [Serializable]
    public class Encode_speech : RAD_wf_Command
    {

        public override int Run()
        {
            //check if any higher level detects any error and if yes, break
            if (base.Run() != 1)
            {
                return -1;
            }

            //execute the actual function
            return _Encode_speech();
        }

        private int _Encode_speech()
        {
            RAD_Settings settings = (RAD_Settings)base.Parent;

            try
            {
                // GENERATING LIPSYNC ANIMATIONS
                if (Directory.Exists(settings.DIR_PHONEMES()))
                {
                    string file = Directory.GetFiles(settings.DIR_PHONEMES(), "*.phonemes", SearchOption.AllDirectories).First();

                    string arg = $"--create-lipsync {file} " +
                                $"--output-dir {settings.DIR_AUDIO_WEM()}" +
                                $"--repo-dir {settings.dir_repo_lipsync()}" +
                               $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech_lipsync_creator() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
                }

                // ENCODING LIPSYNC ANIMATIONS TO CR2W
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    string arg = $"--encode-cr2w {settings.DIR_AUDIO_WEM()} " +
                              $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
                }

                // CREATING W3SPEECH FILE
                if (Directory.Exists(settings.DIR_AUDIO_WEM()))
                {
                    string arg = $"--pack-w3speech {settings.DIR_AUDIO_WEM()} " +
                                $"--output-dir {settings.DIR_DLC_CONTENT()}" +
                                $"--language {settings.language}" +
                              $"{settings.enumToArg(settings.LOG_LEVEL)}";
                    //call encoder FIXME
                    RAD_Task th = new RAD_Task(settings.DIR_ENCODER);
                    var encoder = new w3speech() { Args = arg };
                    th.RunArgs(encoder.ToString(), encoder.Args);
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
                return -1;
                throw;
            }

            return 1;
        }
    }

    

}
