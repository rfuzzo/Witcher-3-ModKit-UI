﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Configuration;
using w3tools.common;

namespace wcc.core.Commands
{
    #region Wcc_lite Command class
    public abstract class WCC_Command: WorkflowItem
    {
        [Browsable(false)]
        public new string Image { get; set; } = "/w3tools.UI;component/Resources/witcher3_101.ico";
        public new ECommandCategory Category { get; set; } = ECommandCategory.Wcc;


        #region Properties

        [CategoryAttribute("0 INFO")]
        [ReadOnly(true)]
        public string CommandLine => ConstructArgs();

        #endregion

        #region Overrides
        public override string ToString() => Name;
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged("CommandLine");
            base.OnPropertyChanged(propertyName);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the wcc lite command
        /// </summary>
        /// <returns></returns>
        public WFR Run() => WFR.WFR_Finished;
        /// <summary>
        /// returns a string constructed from the variables of the wcc_lite command class
        /// </summary>
        /// <returns>string commandline arguments</returns>
        private string ConstructArgs()
        {
            var str_variables = GetVariables();
            string procArgs = ToString() + " ";

            foreach (KeyValuePair<string, string> str in str_variables)
            {
                if (str.Key.Equals("HIDDEN") || String.IsNullOrEmpty(str.Key))
                {
                    procArgs += $"{str.Value} ";
                }
                else
                {
                    procArgs += $"-{str.Key}{str.Value} ";
                }
            }

            return procArgs;
        }

        /// <summary>
        /// returns all variables as dictionary.
        /// </summary>
        /// <returns> Dictionary<string,string> </returns>
        private Dictionary<string, string> GetVariables() //TODO: CLeanup
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            var dict = new Dictionary<string, string>();

            //get all properties with a RADName
            IEnumerable<PropertyInfo> REDarguments = this.GetType().GetProperties(bindingFlags);
            foreach (var pi in REDarguments)
            {
                // initial check
                //check RADName attribute
                REDName REDatt = (REDName)Attribute.GetCustomAttribute(pi, typeof(REDName));
                if (REDatt == null || String.IsNullOrEmpty(REDatt.name))
                {
                    continue;
                }
                //check values
                var val = Convert.ToString(pi.GetValue(this));
                if (String.IsNullOrEmpty(val) || (pi.PropertyType == typeof(bool) && !Boolean.Parse((string)val)))
                {
                    continue;
                }

                string nam = REDatt.name;

                //Strings
                if (pi.PropertyType == typeof(string))
                {
                    REDTags tag = (REDTags)Attribute.GetCustomAttribute(pi, typeof(REDTags));

                    // Paths
                    if (Attribute.GetCustomAttributes(pi).Where(x => (x is REDTags)).Any()
                        && tag.tag.Contains("Path")
                        && val.First() != '"') //check for alrady declared paths
                    {

                        if (Path.GetExtension(val) == "") //is a directory
                        {
                            val = Path.GetFullPath(val).ToString() + "\\";
                            val = val.Replace(@"\", @"\\"); //FIXME? it's stupid but seems to work
                        }
                        val = $"=\"{val}\"";
                    }
                    // other strings
                    else
                    {
                        val = $"={val}";
                    }
                }
                else if (pi.PropertyType == typeof(bool))
                {
                    val = "";
                }
                else
                {
                    if (val == "None")
                    {
                        continue;
                    }
                    else
                    {
                        REDTags tag = (REDTags)Attribute.GetCustomAttribute(pi, typeof(REDTags));

                        // keywords only have values (e.g. analyze r4gui -out=)
                        if (tag != null && tag.tag.Contains("Keyword"))
                        {
                            nam = "HIDDEN";
                        }
                        else
                        {
                            val = $"=\"{val}\"";
                        }
                    }
                }

                dict.Add(nam, val);
            }
            return dict;
        }

        /// <summary>
        /// Function to pass the outfile variable - if existing. 
        /// </summary>
        private string GetOutfile()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            string ret = "";

            foreach (PropertyInfo prop in this.GetType().GetProperties(bindingFlags).Where(x => x.Name.Equals("outfile")))
            {
                var val = Convert.ToString(prop.GetValue(this));
                if (prop.PropertyType == typeof(string) && !String.IsNullOrEmpty(val))
                    ret = val;
            }
            return ret;
        }
        #endregion

    }
    #endregion



}
