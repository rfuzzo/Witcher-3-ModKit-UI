using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;

namespace w3tools
{
    /// <summary>
    /// Radish Workflow Global Settings
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RAD_Settings : ObservableObject
    {
        public RAD_Settings(string w3, string modkit, string encoder, WccExtendedLogger logger)
        {
            DIR_W3 = w3; //game path
            DIR_MODKIT = modkit; //wcc_lite.exe path
            DIR_ENCODER = encoder; //path to directory with encoder binaries
            LOGGER = logger;
            PATCH_MODE = true;
            //LastResult = WFR.WFR_Finished; // first setup is good
        }

        #region Properties
        // Editor Settings
        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_W3 { get; set; }
        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_MODKIT { get; set; }
        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_ENCODER { get; set; }
        [BrowsableAttribute(false)]
        public WccExtendedLogger LOGGER { get; set; }

        // User Settings
        [CategoryAttribute("1 Settings")]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_PROJECT_BASE { get; set; }
        [CategoryAttribute("1 Settings")]
        public string MODNAME { get; set; }
        [CategoryAttribute("1 Settings")]
        public uint idspace { get; set; }
        [CategoryAttribute("1 Settings")]
        public bool auto_delete_mod { get; set; }
        [CategoryAttribute("1 Settings")]
        public ERL LOG_LEVEL { get; set; }
        [CategoryAttribute("1 Settings")]
        public string language { get; set; } = "en"; //FIXME

        // User Flags
        [BrowsableAttribute(false)]
        public bool _PATCH_MODE { get; set; }
        [CategoryAttribute("2 Flags")]
        public bool PATCH_MODE
        { 
            get
            {
                return _PATCH_MODE;
            }
            set
            {
                if (_PATCH_MODE != value)
                {
                    _PATCH_MODE = value;
                    if (_PATCH_MODE)
                        FULL_REBUILD = false;
                    else
                        FULL_REBUILD = true;

                    OnPropertyChanged();
                    OnPropertyChanged("FULL_REBUILD");
                }
            }
        }

        [BrowsableAttribute(false)]
        public bool _FULL_REBUILD { get; set; }
        [CategoryAttribute("2 Flags")]
        public bool FULL_REBUILD
        {
            get
            {
                return _FULL_REBUILD;
            }
            set
            {
                if (_FULL_REBUILD != value)
                {
                    _FULL_REBUILD = value;
                    OnPropertyChanged();

                    if (_FULL_REBUILD)
                    {
                        PATCH_MODE = false;

                        ENCODE_WORLD = true;
                        ENCODE_ENVS = true;
                        ENCODE_QUEST = true;
                        ENCODE_STRINGS = true;
                        ENCODE_SCENES = true;
                        ENCODE_SPEECH = true;
                        DEPLOY_SCRIPTS = true;
                        DEPLOY_TMP_SCRIPTS = true;
                        WCC_REPACK_DLC = true;
                        WCC_REPACK_MOD = true;
                        WCC_IMPORT_MODELS = true;
                    }
                    else
                    {
                        PATCH_MODE = true;

                        ENCODE_WORLD = false;
                        ENCODE_ENVS = false;
                        ENCODE_QUEST = false;
                        ENCODE_STRINGS = false;
                        ENCODE_SCENES = false;
                        ENCODE_SPEECH = false;
                        DEPLOY_SCRIPTS = false;
                        DEPLOY_TMP_SCRIPTS = false;
                        WCC_REPACK_DLC = false;
                        WCC_REPACK_MOD = false;
                        WCC_IMPORT_MODELS = false;
                    }

                    OnPropertyChanged("PATCH_MODE");

                    OnPropertyChanged("ENCODE_WORLD");
                    OnPropertyChanged("ENCODE_ENVS");
                    OnPropertyChanged("ENCODE_QUEST");
                    OnPropertyChanged("ENCODE_STRINGS");
                    OnPropertyChanged("ENCODE_SCENES");
                    OnPropertyChanged("ENCODE_SPEECH");
                    OnPropertyChanged("DEPLOY_SCRIPTS");
                    OnPropertyChanged("DEPLOY_TMP_SCRIPTS");
                    OnPropertyChanged("WCC_REPACK_DLC");
                    OnPropertyChanged("WCC_REPACK_MOD");
                    OnPropertyChanged("WCC_IMPORT_MODELS");
                }
            }
        }

        // Hidden Settings
        #region HIDDEN
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_WORLD { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_ENVS { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_SCENES { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_QUEST { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_STRINGS { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool ENCODE_SPEECH { get; set; }


        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_IMPORT_MODELS { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_COOK { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_OCCLUSIONDATA { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_NAVDATA { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_TEXTURECACHE { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_COLLISIONCACHE { get; set; }
        //[BrowsableAttribute(false)]
        //[CategoryAttribute("3 Hidden")]
        //public bool WCC_SHADERCACHE { get; set; }
        // [BrowsableAttribute(false)]
        //[CategoryAttribute("3 Hidden")]
        //public bool WCC_DEPCACHE { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_ANALYZE { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_ANALYZE_WORLD { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_REPACK_DLC { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool WCC_REPACK_MOD { get; set; }




        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool DEPLOY_SCRIPTS { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool DEPLOY_TMP_SCRIPTS { get; set; }
        [BrowsableAttribute(true)]
        [CategoryAttribute("3 Hidden")]
        public bool START_GAME { get; set; }
        #endregion

        //Constants
        [BrowsableAttribute(false)]
        [CategoryAttribute("4 Constants")]
        public string WORLD_DEF_PREFIX {get;} = "world";
        [BrowsableAttribute(false)]
        [CategoryAttribute("4 Constants")]
        public string ENV_DEF_PREFIX { get; } = "env";
        [BrowsableAttribute(false)]
        [CategoryAttribute("4 Constants")]
        public string SCENE_DEF_PREFIX { get; } = "scene";
        [BrowsableAttribute(false)]
        [CategoryAttribute("4 Constants")]
        public string STRINGS_PART_PREFIX { get; } = "strings";
        [BrowsableAttribute(false)]
        [CategoryAttribute("4 Constants")]
        public string SEEDFILE_PREFIX { get; } = "seed";
        #endregion


        // auto generated
        #region AUTO


        // settings for modkit
        public string DIR_MODKIT_BIN()
        {
            return Path.Combine(DIR_MODKIT, @"..\"); //FIXME
        }
        public string DIR_MODKIT_DEPOT()
        {
            return Path.Combine(DIR_MODKIT, @"..\..\r4data"); //FIXME
        }

        // settings for encoders
        public string dir_repo_quests()
        {
            return Path.Combine(DIR_ENCODER, "repo.quests");
        }
        public string dir_repo_worlds()
        {
            return Path.Combine(DIR_ENCODER, "repo.quests");
        }
        public string dir_repo_scenes()
        {
            return Path.Combine(DIR_ENCODER, "repo.scenes");
        }
        public string dir_repo_lipsync()
        {
            return Path.Combine(DIR_ENCODER, "repo.lipsync");
        }
        public string dir_data_phoneme_generation()
        {
            return Path.Combine(DIR_ENCODER, "data");
        }


        // some environment settings
       public string DIR_PROJECT_BIN()
        {
            return Path.Combine(DIR_PROJECT_BASE, "bin");
        }
        public string DIR_RESOURCES()
        {
            return Path.Combine(DIR_PROJECT_BASE, "resources");
        }
        public string MODNAME_LC()
        {
            return MODNAME.ToLower();
        }

        // output directories
        public string DIR_TMP()
        {
            return Path.Combine(DIR_PROJECT_BASE, "_tmp");
        }
        public string DIR_UNCOOKED()
        {
            return Path.Combine(DIR_PROJECT_BASE, "uncooked");
        }
        public string DIR_UNCOOKED_TEXTURES()
        {
            return Path.Combine(DIR_PROJECT_BASE, "textures");
        }

        public string DIR_COOKED_MOD()
        {
            return Path.Combine(DIR_RESOURCES(), $"mod{MODNAME_LC()}","files"); //FIXME
        }
        public string DIR_COOKED_DLC()
        {
            return Path.Combine(DIR_RESOURCES(), $"dlc{MODNAME_LC()}", "files"); //FIXME
        }

        public string DIR_COOKED_FILES_DB()
        {
            return Path.Combine(DIR_TMP(), "files.cook.db");
        }
        public string DIR_COOKED_TEXTURES_DB()
        {
            return Path.Combine(DIR_TMP(), "textures.cook.db");
        }

        public string DIR_DLC_GAMEPATH()
        {
            return $"dlc\\dlc{MODNAME_LC()}";
        }
        public string DIR_OUTPUT_QUEST()
        {
            return $"{DIR_UNCOOKED()}";
        }
        public string DIR_OUTPUT_SCENES()
        {
            return $"{DIR_UNCOOKED()}\\{DIR_DLC_GAMEPATH()}\\data\\scenes";
        }
        public string DIR_OUTPUT_WORLD()
        {
            return $"{DIR_UNCOOKED()}\\{DIR_DLC_GAMEPATH()}\\levels";
        }
        public string DIR_OUTPUT_ENVS()
        {
            return $"{DIR_UNCOOKED()}\\{DIR_DLC_GAMEPATH()}\\data\\envs";
        }
        public string DIR_OUTPUT_MESHES()
        {
            return $"{DIR_UNCOOKED()}\\{DIR_DLC_GAMEPATH()}\\data\\entities\\meshes";
        }

        public string DIR_DLC()
        {
            return Path.Combine(DIR_W3, $"{DIR_DLC_GAMEPATH()}");
        }
        public string DIR_MOD()
        {
            return Path.Combine(DIR_W3, $"mods\\mod{MODNAME}");
        }
        public string DIR_TMP_MOD()
        {
            return Path.Combine(DIR_W3, $"mods\\mod{MODNAME}_tmp");
        }
        public string DIR_DLC_CONTENT()
        {
            return Path.Combine(DIR_DLC(), "content");
        }
        public string DIR_MOD_CONTENT()
        {
            return Path.Combine(DIR_MOD(), "content");
        }
        public string DIR_TMP_MOD_CONTENT()
        {
            return Path.Combine(DIR_TMP_MOD(), "content");
        }


        // script src dirs
        public string DIR_MOD_SCRIPTS()
        {
            return Path.Combine(DIR_PROJECT_BASE, "mod.scripts");
        }
        public string DIR_TMP_MOD_SCRIPTS()
        {
            return Path.Combine(DIR_PROJECT_BASE, "mod.scripts-tmp");
        }




        // w3strings settings
        public string DIR_STRINGS()
        {
            return Path.Combine(DIR_PROJECT_BASE, "strings");
        }
        

        // w2scene settings
        public string DIR_DEF_SCENES()
        {
            return Path.Combine(DIR_PROJECT_BASE, "definition.scenes");
        }
        

        // w2quest settings
        public string DIR_DEF_QUEST()
        {
            return Path.Combine(DIR_PROJECT_BASE, "definition.quest");
        }
        

        // w3speech settings
       
        public string DIR_SPEECH()
        {
            return Path.Combine(DIR_PROJECT_BASE, "speech");
        }
        public string DIR_AUDIO_WAV()
        {
            return Path.Combine(DIR_SPEECH(), $"speech.{language}.wav");
        }
        public string DIR_PHONEMES()
        {
            return DIR_AUDIO_WAV();
        }
        public string DIR_AUDIO_WEM()
        {
            return Path.Combine(DIR_SPEECH(), $"speech.{language}.wem");
        }

        // w3world settings
        public string DIR_DEF_WORLD()
        {
            return Path.Combine(DIR_PROJECT_BASE, "definition.world");
        }
        

        // w3envs settings
        public string DIR_DEF_ENVS()
        {
            return Path.Combine(DIR_PROJECT_BASE, "definition.envs");
        }
        

        // model import settings
        public string DIR_MODEL_FBX()
        {
            return Path.Combine(DIR_PROJECT_BASE, "models");
        }
        

        // game relative path to worlds for scanning depot
        public string DIR_WCC_DEPOT_WORLDS()
        {
            return Path.Combine(DIR_DLC_GAMEPATH(), "levels");
        }

        #endregion

        /// <summary>
        /// checks the settings for errors
        /// </summary>
        /// <returns></returns>
        public bool CheckSelf()
        {
            bool test_DIR_W3 = Directory.Exists(this.DIR_W3);
            bool test_DIR_MODKIT = File.Exists(this.DIR_MODKIT);
            bool test_DIR_ENCODER = Directory.Exists(this.DIR_ENCODER);

            bool test_DIR_PROJECT_BASE = Directory.Exists(this.DIR_PROJECT_BASE);
            bool test_MODNAME = !String.IsNullOrEmpty(this.MODNAME);
            bool test_idspace = this.idspace > 0 && this.idspace < 9999;

            if (!(test_DIR_W3 && test_DIR_MODKIT && test_DIR_ENCODER))
            {
                // check global settings
                // FIXME Error Message
                LOGGER.LogString($"--------------------------------------------------------------------------");
                LOGGER.LogString($"-- FATAL -- test_DIR_W3: {test_DIR_W3} && test_DIR_MODKIT: {test_DIR_MODKIT} && test_DIR_ENCODER: {test_DIR_ENCODER}");
                LOGGER.LogString($"--------------------------------------------------------------------------");

                return false;
            }
            else if (!(test_DIR_PROJECT_BASE && test_MODNAME && test_idspace))
            {
                //check mod settings
                // FIXME Error Message
                LOGGER.LogString($"--------------------------------------------------------------------------");
                LOGGER.LogString($"-- FATAL -- test_DIR_PROJECT_BASE: {test_DIR_PROJECT_BASE} && test_MODNAME: {test_MODNAME} && test_idspace: {test_idspace}");
                LOGGER.LogString($"--------------------------------------------------------------------------");


                return false;
            }
            else
            {
                // all true
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string EnumToArg(ERL v)
        {
            switch (v)
            {
                case ERL.ERL_verbose: return "--verbose";
                case ERL.ERL_veryverbose: return "--very-verbose";
                case ERL.ERL_empty:
                default: return "";
            }
        }
    }
}
