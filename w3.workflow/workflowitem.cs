using System;
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
    public interface IWorkflowItem
    {

        #region Properties
        string Name { get; set; }
        string Image { get; }
        //FIXME
        object Parent { get; set; } //holds the document Workspace Settings //FIXME
        bool IsVisible { get; set; }

        #endregion

        WFR Run();

    }
}
