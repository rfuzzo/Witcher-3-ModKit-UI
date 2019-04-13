using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using wcc.core.Commands;
using radish.core.Commands;
using w3tools.common;
using w3tools;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;
using w3tools.Workflows;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace w3tools
{
    /// <summary>
    /// Minimal saved data of a rwx Document class
    /// </summary>
    public class RwxData
    {
        public string Title { get; set; }
        public XElement Xsettings { get; set; }
        public List<string> WorkflowItems {get;set;}

        public RwxData(string title, XElement settings, List<string> workflow)
        {
            Title = title;
            Xsettings = settings;
            WorkflowItems = workflow;

        }
        /// <summary>
        /// 
        /// </summary>
        public void Serialize(string file)
        {
            try
            {
                //serialize workflow commands
                XElement xworkflow = new XElement("Workflow");
                foreach (var item in WorkflowItems)
                {
                    xworkflow.Add(new XElement("Name", item ));
                }

                //serialize title and items to xml
                XDocument doc = new XDocument(
                    new XElement("w3ToolsWorkflow",
                        new XElement("Title", Title),
                        xworkflow,
                        Xsettings
                    )
                );

                doc.Save(file);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RwxData Deserialize(string file)
        {
            try
            {
                XDocument doc = XDocument.Load(file);
                if (doc.Elements("w3ToolsWorkflow").Any())
                {
                    var el = doc.Element("w3ToolsWorkflow");

                    //get title (string)
                    string Title = el.Element("Title").Value;

                    //get Setttings (RAD_Settings)
                    XElement Settings = el.Element("WorkflowSettings");


                    //get Workflows (RAD_Settings)
                    XElement xworkflow = el.Element("Workflow");
                    List<string> xitems = xworkflow.Elements().Select(x => x.Value).ToList();

                    return new RwxData(Title, Settings, xitems);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }



    [Serializable]
    public class WccUIVariable
    {
        public string Name { get; set; }

        [Editor(typeof(PropertyGridFolderPicker), typeof(PropertyGridFolderPicker))]
        public string Value { get; set; }

        [ReadOnly(true)]
        public string Parent { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    
    
    

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
            /// TOOL Commands
            #region Tools
            //Wcc
            #region Wcc
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

            // Radish Commands
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
            #endregion

            /// Workflow Commands
            
            #region Workflow Commands
            // Windows Commands
            #region Win
            Add(new CleanupFolder());
            Add(new DeployModScripts());
            Add(new DeployTmpModScripts());
            #endregion

            // Radish Steps
            #region radish
            Add(new Encode_env());
            Add(new Encode_quest());
            Add(new Encode_scene());
            Add(new Encode_speech());
            Add(new Encode_strings());
            Add(new Encode_world());
            #endregion

            // Wcc Steps
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
            #endregion

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
