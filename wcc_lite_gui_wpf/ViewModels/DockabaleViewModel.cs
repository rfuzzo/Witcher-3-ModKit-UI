
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using wcc_lite_gui_wpf.Commands;

namespace wcc_lite_gui_wpf.ViewModels
{
    public class DockabaleViewModel : ObservableObject
    {
        #region Title
        private string _title;
        public virtual string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if(_title != value)
                {
                    _title = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        #region ContentId
        private string _contentId;
        public string ContentId
        {
            get
            {
                return _contentId;
            }
            set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        #region IsActive
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        #region IconSource
        private ImageSource _iconSource;
        public virtual ImageSource IconSource
        {
            get
            {
                return _iconSource;
            }
            set
            {
                if (_iconSource != value)
                {
                    _iconSource = value;
                    InvokePropertyChanged();
                }
            }
        }
        #endregion

        public ICommand SelectCommand { get; }

        public DockabaleViewModel()
        {
            SelectCommand = new RelayCommand(() => IsActive = true);


        }
    }
}