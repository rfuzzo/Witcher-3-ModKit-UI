using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.common
{

    public enum SystemLogFlag
    {
        SLF_Default,
        SLF_Error,
        SLF_Warning,
        SLF_Info,
        SLF_Interpretable
    }
    public enum WccLogFlag
    {
        WLF_Default,
        WLF_Error,
        WLF_Warning,
        WLF_Info
    }
    public enum WccLoggerSingleStatus
    {
        LSS_Idle,
        LSS_Finished,
        LSS_FinishedWithErrors,
        LSS_FinishedWithWarnings
    }


    public struct ExtendedWCCLogMessage
    {
        public int Id { get; set; }

        // global flags
        public SystemLogFlag SystemFlag { get; set; }
        public string WccCommandName { get; set; }

        // Interpreted Wcc Log Messages
        // wc auto flags
        public string Timestamp { get; set; }
        public WccLogFlag WccFlag { get; set; }
        public string WccModule { get; set; }

        // Message
        public string Value { get; set; }
    }

    /// <summary>
    /// Logger Class
    /// </summary>
    #region Logger
    public class WccExtendedLogger : ObservableObject
    {
        public WccExtendedLogger()
        {
            RawLog = new ObservableCollection<string>();
            ExtendedLog = new ObservableCollection<ExtendedWCCLogMessage>();
        }

        #region Properties

        private int _progressValue;
        /// <summary>
        /// Progress Value for Progress Bar.
        /// </summary>
        public int ProgressValue
        {
            get
            {
                return _progressValue;
            }
            set
            {
                if (_progressValue != value)
                {
                    _progressValue = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isIndeterminate;
        /// <summary>
        /// Indeterminate Status for Progress Bar.
        /// </summary>
        public bool IsIndeterminate
        {
            get
            {
                return _isIndeterminate;
            }
            set
            {
                if (_isIndeterminate != value)
                {
                    _isIndeterminate = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Overall Status of the Logger.
        /// </summary>
        public WccLoggerSingleStatus Status
        {
            get
            {
                bool hasErrors = ExtendedLog.Where(x => x.WccFlag.Equals(WccLogFlag.WLF_Error)).Any();
                bool hasWarning = ExtendedLog.Where(x => x.WccFlag.Equals(WccLogFlag.WLF_Warning)).Any();

                if (!RawLog.Any())
                    return WccLoggerSingleStatus.LSS_Idle;

                if (hasErrors)
                    return WccLoggerSingleStatus.LSS_FinishedWithErrors;
                else if (hasWarning)
                    return WccLoggerSingleStatus.LSS_FinishedWithWarnings;
                else
                    return WccLoggerSingleStatus.LSS_Finished;
            }

        }

        private string _Log;
        public string Log
        {
            get
            {
                return _Log;
            }
            set
            {
                if (_Log != value)
                {
                    _Log = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> RawLog { get; set; }
        public ObservableCollection<ExtendedWCCLogMessage> ExtendedLog { get; set; }

        #endregion

        public void ClearLog()
        {
            ExtendedLog.Clear();
            RawLog.Clear();
            Log = "";

        }
        public void NotifyStatusChanged()
        {
            OnPropertyChanged("Status");
        }

        /// <summary>
        /// Log simple strings
        /// </summary>
        public void LogString(string value)
        {
            RawLog.Add(value);
            Log += value + "\r\n";
            
        }
        public void LogStrings(ObservableCollection<string> innerlog)
        {
            foreach (string item in innerlog)
            {
                RawLog.Add(item);
                Log += item + "\r\n";
            }
        }

        /// <summary>
        /// Log an Interpretable LogMessage
        /// </summary>
        public void LogExtended(SystemLogFlag sflag, string cmdName, string value)
        {
            if (sflag == SystemLogFlag.SLF_Interpretable)
            {
                InterpretLogMessage(sflag, cmdName, value);
            }
        }
        public void LogExtendedCollection(ObservableCollection<ExtendedWCCLogMessage> innerlog)
        {
            List<ExtendedWCCLogMessage> oldItems = ExtendedLog.ToList();
            foreach (ExtendedWCCLogMessage item in innerlog)
            {
                oldItems.Add(item);
            }
            ExtendedLog.Clear();
            for (int i = 0; i < oldItems.Count; i++)
            {
                var curitem = oldItems[i];
                curitem.Id = i;
                ExtendedLog.Add(curitem);
            }
        }


        private void InterpretLogMessage(SystemLogFlag sflag, string cmdName, string value)
        {
            ExtendedWCCLogMessage data = new ExtendedWCCLogMessage
            {
                SystemFlag = sflag,
                WccCommandName = cmdName
            };

            try
            {
                // read timestamp
                int flagEnd = value.IndexOf(']');
                string timestamp = value?.Substring(1, flagEnd - 1);
                value = value?.Remove(0, flagEnd + 1);
                data.Timestamp = timestamp;

                // read WccFlag
                flagEnd = value.IndexOf(']');
                string wflag = value?.Substring(1, flagEnd - 1);
                value = value?.Remove(0, flagEnd + 1);
                data.WccFlag = GetWFlagFromString(wflag);

                // read Module
                flagEnd = value.IndexOf(']');
                string module = value?.Substring(1, flagEnd - 1);
                value = value?.Remove(0, flagEnd + 1);
                data.WccModule = module;

                // read LogMessage
                string message = value?.Substring(1);
                data.Value = message;

            }
            catch (Exception)
            {
                data.WccFlag = WccLogFlag.WLF_Info;
                data.WccModule = "Verbose";
                data.Value = value;
                //ExtendedLog.Add(data);
            }
            if (data.WccFlag != WccLogFlag.WLF_Info)
                ExtendedLog.Add(data);
        }

        private WccLogFlag GetWFlagFromString(string wflag)
        {
            switch (wflag)
            {
                case "Warning": return WccLogFlag.WLF_Warning;
                case "Error": return WccLogFlag.WLF_Error;
                case "Info": return WccLogFlag.WLF_Info;
                default: return WccLogFlag.WLF_Default;
            }
        }
    }





    #endregion


}
