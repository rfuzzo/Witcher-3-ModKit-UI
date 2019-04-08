﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.core;
using Xceed.wpf.PropertyGrid.Extensions.EditorTemplates;

namespace w3tools.UI
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
