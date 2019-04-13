using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace w3tools.UI
{
    /// <summary>
    /// Holds all referenced icons
    /// </summary>
    #region Icons
    public class IconsCollection
    {
        public readonly Icon ErrorIcon = Properties.Resources.StatusCriticalError_16x;
        public readonly Icon WarningIcon = Properties.Resources.StatusWarning_16x;
        public readonly Icon InfoIcon = Properties.Resources.StatusInformation_16x;
    }

    #endregion
}
