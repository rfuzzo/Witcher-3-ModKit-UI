using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core;
using wcc.core.Commands;
using radish.core.Commands;
using System.ComponentModel;
using w3.workflow;
using w3.tools.Commands;

namespace w3.tools
{
    [Serializable]
    public abstract class RAD_Workflow : ObservableObject
    {

        [BrowsableAttribute(false)]
        [CategoryAttribute("General")]
        public WccCommandCategory Category { get; set; }
        [BrowsableAttribute(false)]
        [CategoryAttribute("General")]
        public WccCommandCategory DefaultCategory { get; }

        [BrowsableAttribute(false)]
        [CategoryAttribute("General")]
        public string Name { get; set; }



        [BrowsableAttribute(false)]
        [CategoryAttribute("General")]
        public List<WorkflowItem> Steps { get; set; }


        public RAD_Workflow()
        {
            Steps = new List<WorkflowItem>();
        }

        /// <summary>
        /// Overrides
        /// </summary>
        #region Overrides
        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
        #endregion

        /// <summary>
        /// Reset's the commands category to it's  default.
        /// </summary>
        public void ResetCategory()
        {
            Category = DefaultCategory;
        }
    }

    public class RAD_Workflow_BuildAll : RAD_Workflow
    {
        public RAD_Workflow_BuildAll()
        {
            Name = "Build All";

            Steps.Add(new CleanupFolder());
            Steps.Add(new DeployModScripts());
            Steps.Add(new DeployTmpModScripts());

            Steps.Add(new Encode_world());
            // route output to cooked folder if cooking is not flagged //FIXME
            Steps.Add(new Encode_env());
            Steps.Add(new Encode_scene());
            Steps.Add(new Encode_quest());

            Steps.Add(new import());
            Steps.Add(new pathlib());
            Steps.Add(new cookocclusion());
            // prepare cooking
            Steps.Add(new analyze());
            Steps.Add(new cook());
            // prepare packing
            Steps.Add(new pack()); //mod
            Steps.Add(new metadatastore()); //mod
            Steps.Add(new pack()); //dlc
            Steps.Add(new metadatastore()); //dlc

            Steps.Add(new Encode_strings());
            Steps.Add(new Encode_speech());

            Steps.Add(new buildcache()); //texture
            Steps.Add(new buildcache()); //collision

        }



    }

}
