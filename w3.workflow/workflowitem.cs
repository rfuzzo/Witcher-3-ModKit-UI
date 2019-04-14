﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.common
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
        public WorkflowItem(WccCommandCategory defaultCategory = WccCommandCategory.Default)
        {
            DefaultCategory = defaultCategory;
            Category = defaultCategory;
        }

        #region Properties
        [BrowsableAttribute(false)]
        public string Name { get; set; }

        [BrowsableAttribute(false)]
        public string Image { get; set; }

        [BrowsableAttribute(false)]
        public WccCommandCategory Category { get; set; }
        [BrowsableAttribute(false)]
        public WccCommandCategory DefaultCategory { get; set; }


        // need a way to access the workflow settings that are stored
        // in command > list<commands> > workflow > workflowViewModel < settings
        [BrowsableAttribute(false)]
        public object Parent { get; set; } //holds the document Workspace Settings //FIXME


        #endregion


        /// <summary>
        /// Reset's the commands category to it's  default.
        /// </summary>
        public void ResetCategory()
        {
            Category = DefaultCategory;
        }
        public virtual WFR Run()
        {
            //FIXME
            return WFR.WFR_Finished;
        }
        /// <summary>
        /// Overrides
        /// </summary>
        /// //FIXME?
        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        #endregion


       


    }
}
