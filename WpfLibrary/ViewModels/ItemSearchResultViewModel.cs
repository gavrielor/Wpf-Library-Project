using ClassLibraryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class ItemSearchResultViewModel : ViewModelBase
    {
        private List<AbstractItem> resultList;

        public List<AbstractItem> ResultList { get => resultList; set => Set(ref resultList, value); }
        public RelayCommand HomeCommand { get; set; }

        public ItemSearchResultViewModel()
        {
            HomeCommand = new RelayCommand(Navigation.Welcome);
        }
    }
}
