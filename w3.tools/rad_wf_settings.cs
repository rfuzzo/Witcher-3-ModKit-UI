using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;

namespace w3.tools
{
    public enum ERL
    {
        empty,
        verbose,
        veryverbose
    }

    


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class RAD_Settings
    {

        public string enumToArg(ERL v)
        {
            switch (v)
            {
                case ERL.verbose: return "--verbose";
                case ERL.veryverbose: return "--very-verbose";
                case ERL.empty: 
                default: return "";
            }
        }

        public RAD_Settings(string w3, string modkit, string encoder)
        {
            DIR_W3 = w3;
            DIR_MODKIT = modkit; //wcc_lite.exe path
            DIR_ENCODER = encoder;
        }

        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_W3 { get; set; }
        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_MODKIT { get; set; }
        [BrowsableAttribute(false)]
        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string DIR_ENCODER { get; set; }





        // requires user input
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

        // default flags for build steps: do nothing
        #region FLAGS




        [CategoryAttribute("2 Flags")]
        public bool PATCH_MODE { get; set; }
        [CategoryAttribute("2 Flags")]
        public bool FULL_REBUILD { get; set; }
        //[CategoryAttribute("2 Flags")]
        //public bool SKIP_WORLD_GENERATION { get; set; }
        //[CategoryAttribute("2 Flags")]
        //public bool SKIP_FOLIAGE_GENERATION { get; set; }
        
        [CategoryAttribute("2 Flags")]
        public string MODELNAME { get; set; }

        [CategoryAttribute("3 Flags")]
        public bool ENCODE_WORLD { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool ENCODE_ENVS { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool ENCODE_SCENES { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool ENCODE_QUEST { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool ENCODE_STRINGS { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool ENCODE_SPEECH { get; set; }


        [CategoryAttribute("3 Flags")]
        public bool WCC_IMPORT_MODELS { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_SEEDFILES { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_COOK { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_OCCLUSIONDATA { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_NAVDATA { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_TEXTURECACHE { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_COLLISIONCACHE { get; set; }
        //[CategoryAttribute("3 Flags")]
        //public bool WCC_SHADERCACHE { get; set; }
        //[CategoryAttribute("3 Flags")]
        //public bool WCC_DEPCACHE { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_ANALYZE { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_ANALYZE_WORLD { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_REPACK_DLC { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool WCC_REPACK_MOD { get; set; }




        [CategoryAttribute("3 Flags")]
        public bool DEPLOY_SCRIPTS { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool DEPLOY_TMP_SCRIPTS { get; set; }
        [CategoryAttribute("3 Flags")]
        public bool START_GAME { get; set; }



        //public int ENVID { get; set; }
        //public int SCENEID { get; set; }
        //public string MODELNAME { get; set; }

        #endregion

        // auto generated properties
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
            return Path.Combine(DIR_ENCODER, @"\repo.quests");
        }
        public string dir_repo_worlds()
        {
            return Path.Combine(DIR_ENCODER, @"\repo.quests");
        }
        public string dir_repo_scenes()
        {
            return Path.Combine(DIR_ENCODER, @"\repo.scenes");
        }
        public string dir_repo_lipsync()
        {
            return Path.Combine(DIR_ENCODER, @"\repo.lipsync");
        }
        public string dir_data_phoneme_generation()
        {
            return Path.Combine(DIR_ENCODER, @"\data");
        }


        // some environment settings
       public string DIR_PROJECT_BIN()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\bin");
        }
        public string DIR_RESOURCES()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\resources");
        }
        public string MODNAME_LC()
        {
            return MODNAME.ToLower();
        }

        // output directories
        public string DIR_TMP()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\_tmp");
        }
        public string DIR_UNCOOKED()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\uncooked");
        }
        public string DIR_UNCOOKED_TEXTURES()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\textures");
        }

        public string DIR_COOKED_MOD()
        {
            return Path.Combine(DIR_RESOURCES(), $"mod{MODNAME_LC()}\\files"); //FIXME
        }
        public string DIR_COOKED_DLC()
        {
            return Path.Combine(DIR_RESOURCES(), $"dlc{MODNAME_LC()}\\files"); //FIXME
        }

        public string DIR_COOKED_FILES_DB()
        {
            return Path.Combine(DIR_TMP(), @"\files.cook.db");
        }
        public string DIR_COOKED_TEXTURES_DB()
        {
            return Path.Combine(DIR_TMP(), @"\textures.cook.db");
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
            return Path.Combine(DIR_W3, $"\\{DIR_DLC_GAMEPATH()}");
        }
        public string DIR_MOD()
        {
            return Path.Combine(DIR_W3, $"\\mods\\mod{MODNAME}");
        }
        public string DIR_TMP_MOD()
        {
            return Path.Combine(DIR_W3, $"\\mods\\mod{MODNAME}_tmp");
        }
        public string DIR_DLC_CONTENT()
        {
            return Path.Combine(DIR_DLC(), @"\content");
        }
        public string DIR_MOD_CONTENT()
        {
            return Path.Combine(DIR_MOD(), @"\content");
        }
        public string DIR_TMP_MOD_CONTENT()
        {
            return Path.Combine(DIR_TMP_MOD(), @"\content");
        }


        // script src dirs
        public string DIR_MOD_SCRIPTS()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\mod.scripts");
        }
        public string DIR_TMP_MOD_SCRIPTS()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\mod.scripts-tmp");
        }




        // w3strings settings
        public string DIR_STRINGS()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\strings");
        }
        

        // w2scene settings
        public string DIR_DEF_SCENES()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\definition.scenes");
        }
        

        // w2quest settings
        public string DIR_DEF_QUEST()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\definition.quest");
        }
        public const string SEEDFILE_PREFIX = "seed";

        // w3speech settings
        [CategoryAttribute("1 Settings")]
        public string language { get; set; } = "en"; //FIXME
        public string DIR_SPEECH()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\speech");
        }
        public string DIR_AUDIO_WAV()
        {
            return Path.Combine(DIR_SPEECH(), $"\\speech.{language}.wav");
        }
        public string DIR_PHONEMES()
        {
            return DIR_AUDIO_WAV();
        }
        public string DIR_AUDIO_WEM()
        {
            return Path.Combine(DIR_SPEECH(), $"\\speech.{language}.wem");
        }

        // w3world settings
        public string DIR_DEF_WORLD()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\definition.world");
        }
        

        // w3envs settings
        public string DIR_DEF_ENVS()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\definition.envs");
        }
        

        // model import settings
        public string DIR_MODEL_FBX()
        {
            return Path.Combine(DIR_PROJECT_BASE, @"\models");
        }
        public const string MODEL_PREFIX = "model";

        // game relative path to worlds for scanning depot
        public string DIR_WCC_DEPOT_WORLDS()
        {
            return Path.Combine(DIR_DLC_GAMEPATH(), @"\levels");
        }

        #endregion


        //const?
        //FIXME

    }
}
