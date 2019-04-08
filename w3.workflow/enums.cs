using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3.workflow
{
    /// <summary>
    /// Workflow Result
    /// </summary>
    public enum WFR
    {
        WFR_NotRun = 0,
        WFR_Error = -1,
        WFR_Finished = 1
    }


    /// <summary>
    /// Error Level
    /// </summary>
    public enum ERL
    {
        ERL_empty,
        ERL_verbose,
        ERL_veryverbose
    }
}
