using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using w3tools.common;

namespace w3tools.UI.ViewModels
{
    /// <summary>
    /// Base abstract class for all viewmodels.
    /// </summary>
    public abstract class ViewModel : ObservableObject, IViewModel
    {
        #region Explicit Methods
        protected virtual bool ChangeProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
