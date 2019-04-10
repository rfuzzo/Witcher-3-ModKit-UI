using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core;
using wcc.core.Commands;
using radish.core.Commands;
using System.ComponentModel;
using w3tools.common;

namespace w3tools.Workflows
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

            Steps.Add(new WCC_import());
            Steps.Add(new WCC_GenerateNavData());
            Steps.Add(new WCC_occlusion());

            Steps.Add(new PrepareCooking());

            Steps.Add(new WCC_analyze());
            Steps.Add(new WCC_CookData());

            Steps.Add(new PreparePackaging());

            Steps.Add(new WCC_PackDLCAndCreateMetadatastore());
            Steps.Add(new WCC_PackMODAndCreateMetadatastore());

            Steps.Add(new Encode_strings());
            Steps.Add(new Encode_speech());

            Steps.Add(new WCC_GenerateTextureCache()); //texture
            Steps.Add(new WCC_GenerateCollisionCache()); //collision

        }



    }

}
