using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3.workflow
{

    /// <summary>
    /// 
    /// </summary>
    public enum WccCommandCategory
    {
        Favourites,
        Wcc,
        Radish,
        WF_Windows,
        WF_Radish,
        WF_Wcc,
        Default,
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class WorkflowItem : ObservableObject
    {
        [BrowsableAttribute(false)]
        public string Name { get { return this.ToString(); } }

        [BrowsableAttribute(false)]
        public WccCommandCategory Category { get; set; }
        [BrowsableAttribute(false)]
        public WccCommandCategory DefaultCategory { get; set; }


        public WorkflowItem(WccCommandCategory defaultCategory = WccCommandCategory.Default)
        {
            DefaultCategory = defaultCategory;
            Category = defaultCategory;
        }


        // need a way to access the workflow settings that are stored
        // in command > list<commands> > workflow > workflowViewModel < settings
        [BrowsableAttribute(false)]
        public object Parent { get; set; } //holds the document Workspace Settings //FIXME










        /// <summary>
        /// Overrides
        /// </summary>
        #region Overrides
        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
        #endregion


        public virtual WFR Run()
        {


            //FIXME
            return WFR.WFR_Finished;
        }


    }
}
