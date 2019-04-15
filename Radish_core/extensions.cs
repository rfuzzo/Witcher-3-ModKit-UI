using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using w3tools.common;

namespace radish.core
{
    /// <summary>
    /// Custom Attributes to tag properties in the wcc_lite command task.
    /// </summary>
    #region Custom Attributes
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class RADTags : System.Attribute
    {
        public string[] tag;
        public RADTags(params string[] tag)
        {
            this.tag = tag;
        }
    }
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class RADName : System.Attribute
    {
        public string name;
        public RADName(string name)
        {
            this.name = name;
        }
    }

    #endregion


    public enum language
    {
        None,
        ar,
        br,
        cz,
        de,
        en,
        es,
        esMX,
        fr,
        hu,
        it,
        jp,
        kr,
        pl,
        ru,
        tr,
        zh
    }

}
