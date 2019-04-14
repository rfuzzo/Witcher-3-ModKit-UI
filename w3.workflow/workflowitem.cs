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
    /// Base class for executable commands for Wcc_Lite or the Radish Encoders
    /// </summary>
    public abstract class WorkflowItem : ObservableObject, IWorkflowItem
    {
        public WorkflowItem()
        {
            IsVisible = true;
            IsExpanded = true;
        }

        [BrowsableAttribute(false)]
        private string _name;
        [BrowsableAttribute(false)]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        [BrowsableAttribute(false)]
        private string _image;
        [BrowsableAttribute(false)]
        public string Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }
        [BrowsableAttribute(false)]
        private object _customTag;
        [BrowsableAttribute(false)]
        public object CustomTag
        {
            get => _customTag;
            set
            {
                if (_customTag != value)
                {
                    _customTag = value;
                    OnPropertyChanged();
                }
            }
        }
        [BrowsableAttribute(false)]
        private bool _isExpanded;
        [BrowsableAttribute(false)]
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged();
                }
            }
        }
        [BrowsableAttribute(false)]
        private bool _isVisible;
        [BrowsableAttribute(false)]
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        [BrowsableAttribute(false)]
        public ITreeViewItem ParentTreeViewItem { get; set; }

        public virtual WFR Run()
        {
            return WFR.WFR_Finished;
        }
    }

    /// <summary>
    /// Interface for executable commands for Wcc_Lite or the Radish Encoders
    /// </summary>
    public interface IWorkflowItem : ITreeViewItem
    {

        #region Properties
        string Name { get; set; }
        string Image { get; }
        object CustomTag { get; set; } //holds the document Workspace Settings //FIXME
        #endregion

        WFR Run();

    }

    /// <summary>
    /// Interface for searchable TreeView Items
    /// </summary>
    public interface ITreeViewItem
    {
        bool IsVisible { get; set; }
        bool IsExpanded { get; set; }
        ITreeViewItem ParentTreeViewItem { get; set; } //unused
    }
}
