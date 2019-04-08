using wcc_lite_gui_wpf.Commands;
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
using Ninject;
using Ninject.Infrastructure;

namespace wcc_lite_gui_wpf.ViewModels
{
    public class DockableViewModel : ViewModel
    {
        #region Properties
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
                    OnPropertyChanged();
                }
            }
        }

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
                    OnPropertyChanged();
                }
            }
        }

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
                    OnPropertyChanged();
                }
            }
        }

        private MainViewModel _parentViewModel;
        public MainViewModel ParentViewModel
        {
            get
            {
                return _parentViewModel;
            }
            set
            {
                if (_parentViewModel != value)
                {
                    _parentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public ICommand SelectCommand { get; }

        public DockableViewModel()
        {
            

            SelectCommand = new RelayCommand(() => IsActive = true);
        }

        
    }
}