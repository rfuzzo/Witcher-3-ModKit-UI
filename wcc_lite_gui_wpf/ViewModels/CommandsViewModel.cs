using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wcc_lite_core;
using wcc_lite_gui_wpf.Commands;

namespace wcc_lite_gui_wpf.ViewModels
{
    public class CommandsViewModel : MainViewModel, IViewModel
    {
        private string _selectedCommand;
        public string SelectedCommand
        {
            get { return _selectedCommand; }
            set { ChangeProperty(ref _selectedCommand, value); }
        }


        //FIXME in child View Model 
        public ICommand AddToFavouritesCommand { get; }
        public ICommand RemoveFromfavouritesCommand { get; }



        //FIXME in child View Model 
        public bool CanAddToFavourites()
        {
            return ActiveCommand != null && ActiveCommand.Category != WccCommandCategory.Favourites;
        }
        public void AddToFavourites()
        {
            ActiveCommand.Category = WccCommandCategory.Favourites;
        }
        public bool CanRemoveFromfavourites()
        {
            return ActiveCommand != null && ActiveCommand.Category != WccCommandCategory.Default;
        }
        public void RemoveFromfavourites()
        {
            ActiveCommand.Category = WccCommandCategory.Default;
        }





        public CommandsViewModel()
        {
            //FIXME in child View Model 
            AddToFavouritesCommand = new RelayCommand(AddToFavourites, CanAddToFavourites);
            RemoveFromfavouritesCommand = new RelayCommand(RemoveFromfavourites, CanRemoveFromfavourites);
        }

        
    }
}
