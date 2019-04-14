using radish.core.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;
using w3tools.Workflows;
using wcc.core.Commands;

namespace w3tools
{
   
    public class WCCCommandsCollection : ObservableCollection<IWorkflowItem>
    {
        public WCCCommandsCollection()
        {
            #region Wcc
            Add(new uncook());
            Add(new unbundle());
            Add(new export());
            Add(new import());
            Add(new dumpbundleinfo());
            Add(new dumpfile());
            Add(new buildcache());
            Add(new metadatastore());
            Add(new cook());
            Add(new pack());
            Add(new cookmaterials());
            Add(new package());
            Add(new swfimport());
            Add(new swfdump());
            Add(new analyze());
            Add(new splitcache());
            Add(new reportchunks());
            Add(new dumpscripts());
            Add(new gluefiles());
            Add(new gluefilesdlc());
            Add(new loadtest());
            Add(new validate());
            Add(new get_txts());
            Add(new optimizecollisioncache());
            Add(new patch());
            Add(new r4characters());
            Add(new r4charactersdlc());
            Add(new dumpcharset());
            Add(new venc());
            Add(new WorldSceneDependencyInfoFiles());
            Add(new questlayoutdump());
            Add(new dependencies());
            Add(new exportbundles());
            Add(new filever());
            Add(new resourceusage());
            Add(new cooksubs());
            Add(new cooksounds());
            Add(new split());
            Add(new splitstrings());
            Add(new resave());
            Add(new cookocclusion());
            Add(new calculateRuntimeOcclusionMemory());
            Add(new findDuplicates());
            Add(new pathlib());
            Add(new cookstrings());
            Add(new testcollisioncache());
            Add(new cookertest());
            Add(new testmem());
            Add(new voconvert());
            #endregion
        }
    }
    public class RADCommandsCollection : ObservableCollection<IWorkflowItem>
    {
        public RADCommandsCollection()
        {
            #region radish
            Add(new w3env());
            Add(new w2quest());
            Add(new w2scene());
            Add(new w3speech());
            Add(new w3speech_phoneme_extractor());
            Add(new w3speech_lipsync_creator());
            Add(new w3strings());
            Add(new w3world());
            #endregion
        }
    }
    public class WF_WIN_CommandsCollection : ObservableCollection<IWorkflowItem>
    {
        public WF_WIN_CommandsCollection()
        {
            #region Win
            Add(new CleanupFolder());
            Add(new DeployModScripts());
            Add(new DeployTmpModScripts());
            #endregion
        }
    }
    public class WF_RAD_CommandsCollection : ObservableCollection<IWorkflowItem>
    {
        public WF_RAD_CommandsCollection()
        {
            #region radish
            Add(new Encode_env());
            Add(new Encode_quest());
            Add(new Encode_scene());
            Add(new Encode_speech());
            Add(new Encode_strings());
            Add(new Encode_world());
            #endregion
        }
    }
    public class WF_WCC_CommandsCollection : ObservableCollection<IWorkflowItem>
    {
        public WF_WCC_CommandsCollection()
        {
            #region wcc
            Add(new WCC_import());
            Add(new WCC_occlusion());
            Add(new WCC_analyze());
            Add(new WCC_GenerateNavData());
            Add(new WCC_CookData());
            Add(new WCC_PackDLCAndCreateMetadatastore());
            Add(new WCC_PackMODAndCreateMetadatastore());
            Add(new WCC_GenerateTextureCache());
            Add(new WCC_GenerateCollisionCache());
            #endregion
        }
    }
}
