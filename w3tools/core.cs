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

        }
    }

}
