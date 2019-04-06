using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcc_lite_core;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;

namespace wcc_lite_gui_wpf
{
    

    
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
}
