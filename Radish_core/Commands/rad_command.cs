using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace radish.core.Commands
{
    public abstract class RAD_Command : WorkflowItem
    {
        [Browsable(false)]
        public new string Image { get; set; } = "/w3tools.UI;component/Resources/radish_icon_circle.png";
        public new ECommandCategory Category { get; set; } = ECommandCategory.Radish;

        #region Properties

        [CategoryAttribute("0 INFO")]
        [DescriptionAttribute("FLAG -- Log Help.")]
        [RADName("-h")]
        public bool Help { get; set; }

        [CategoryAttribute("0 INFO")]
        [DescriptionAttribute("ENUM -- logging level for all encoders. uncomment desired level, default is empty -> minimal info + warnings + errors.")]
        public ERL LogLevel { get; set; }

        [CategoryAttribute("0 INFO")]
        [ReadOnly(true)]
        public string Version { get; set; }
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
            string procArgs = "";

            foreach (KeyValuePair<string, string> str in str_variables)
            {
                if (str.Key.Equals("HIDDEN") || String.IsNullOrEmpty(str.Key))
                {
                    procArgs += $"{str.Value} ";
                }
                else
                {
                    procArgs += $"{str.Key} {str.Value} ";
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
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            Dictionary<string, string> dict = new Dictionary<string, string>();

            //get all properties with a RADName
            IEnumerable<PropertyInfo> RADarguments = this.GetType().GetProperties(bindingFlags);
            foreach (var pi in RADarguments)
            {
                // initial check
                //check RADName attribute
                RADName RADatt = (RADName)Attribute.GetCustomAttribute(pi, typeof(RADName));
                if (RADatt == null || String.IsNullOrEmpty(RADatt.name))
                {
                    continue;
                }

                //check values
                var val = Convert.ToString(pi.GetValue(this));
                if (String.IsNullOrEmpty(val) || (pi.PropertyType == typeof(bool) && !Boolean.Parse((string)val)))
                {
                    continue;
                }

                string nam = RADatt.name;
                
                if (pi.PropertyType == typeof(bool))
                {
                    val = "";
                }
                if (pi.PropertyType == typeof(ERL))
                {
                    val = EnumToArg((ERL)pi.GetValue(this));
                }

                dict.Add(nam, val);
            }
            
            return dict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private string EnumToArg(ERL v)
        {
            switch (v)
            {
                case ERL.ERL_verbose: return "--verbose";
                case ERL.ERL_veryverbose: return "--very-verbose";
                case ERL.ERL_release: return "--release-version";
                case ERL.ERL_empty:
                default: return "";
            }
        }
        #endregion

    }
}

