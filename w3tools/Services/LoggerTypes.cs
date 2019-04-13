﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.Services
{
    public enum SystemLogFlag
    {
        SLF_Default,
        SLF_Error,
        SLF_Warning,
        SLF_Info,
        SLF_Interpretable
    }
    public enum LogFlag
    {
        WLF_Default,
        WLF_Error,
        WLF_Warning,
        WLF_Info
    }
    public enum ToolFlag
    {
        TLF_Wcc,
        TLF_Radish,
    }

    public abstract class InterpretedLogMessage
    {
        public int Id { get; set; }

        // global flags
        public SystemLogFlag SystemFlag { get; set; }
        public string CommandName { get; set; }
        public ToolFlag Tool { get; set; }
        public LogFlag Flag { get; set; }


        // Message

        public string Value { get; set; }
    }
    /// <summary>
    /// Interpreted Wcc Log Messages
    /// </summary>
    public class WCCLogMessage : InterpretedLogMessage
    {
        public string Timestamp { get; set; }
        public string WccModule { get; set; }
    }
    /// <summary>
    /// Interpreted Wcc Log Messages
    /// </summary>
    public class RADLogMessage : InterpretedLogMessage
    {
    }
}
