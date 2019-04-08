using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using wcc.core.Commands;
using radish.core.Commands;
using w3.workflow;
using w3.tools.Commands;

namespace w3.tools
{
    
    /*
    /// <summary>
    /// Holds all referenced icons
    /// </summary>
    #region Icons
    public class IconsCollection
    {
        public readonly Icon ErrorIcon = Properties.Resources.StatusCriticalErrorIcon;
        public readonly Icon WarningIcon = Properties.Resources.StatusWarningIcon;
        public readonly Icon InfoIcon = Properties.Resources.StatusInformationIcon;
    }

    #endregion
    */

    /// <summary>
    /// Holds all wcc_lite commands
    /// </summary>
    #region Commands
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WccCommandsCollection : ObservableCollection<WorkflowItem>
    {
        public WccCommandsCollection()
        {
            /// Commmom Conmmands

            Add(new uncook(WccCommandCategory.Favourites));
            Add(new unbundle(WccCommandCategory.Favourites));
            Add(new export(WccCommandCategory.Favourites));
            Add(new import(WccCommandCategory.Favourites));
            Add(new dumpbundleinfo(WccCommandCategory.Favourites));
            Add(new dumpfile(WccCommandCategory.Favourites));
            Add(new buildcache(WccCommandCategory.Favourites));
            Add(new metadatastore(WccCommandCategory.Favourites));
            Add(new cook(WccCommandCategory.Favourites));
            Add(new pack(WccCommandCategory.Favourites));
            Add(new cookmaterials(WccCommandCategory.Favourites));


            /// Uncommon Conmmands
            /// Add(new package();
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


            /// Radish Commands
            Add(new w3env());
            Add(new w2quest());
            Add(new w2scene());
            Add(new w3speech());
            Add(new w3strings());
            Add(new w3world());

            /// Windows Commands
            Add(new CleanupFolder());
            Add(new DeployModScripts());
            Add(new DeployTmpModScripts());



        }
    }

    /// <summary>
    /// Holds all Radish Workflows
    /// </summary>
    public class RadishWorkflowCollection : ObservableCollection<RAD_Workflow>
    {
        public RadishWorkflowCollection()
        {
            /// Commmom Conmmands
            Add(new RAD_Workflow_BuildAll());



            //dbg
            Add(new RAD_Workflow_BuildAll());

            #endregion
        }
    }

}
