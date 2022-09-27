using ClassLibraryLibrary;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class WelcomeViewModel : ViewModelBase
    {
        private Library library;

        public Type SelectedType { get; set; }
        public IEnumerable<Type> AvailableTypes { get; set; }

        public RelayCommand WorkerMenuCommand { get; set; }
        public RelayCommand BrowseCommand { get; set; }
        public RelayCommand ShowAllItemsCommand { get; set; }

        public WelcomeViewModel(Library library)
        {
            this.library = library;

            AvailableTypes = library.GetAllItemTypes();
            SelectedType = AvailableTypes.First();

            WorkerMenuCommand = new RelayCommand(() => Navigation.Worker());
            BrowseCommand = new RelayCommand(Browse);
            ShowAllItemsCommand = new RelayCommand(ShowAllItems);
        }

        private void Browse()
        {
            if (library.Items.Where(item => item.GetType() == SelectedType).Any()) Navigation.ItemSearch();
            else MessageBox.Show("There is no items from the selected type", "Message!");
        }

        private void ShowAllItems()
        {
            var items = library.Items.ToList();
            if (items.Any())
            {
                ViewModelLocator.ItemSearchResult.ResultList = items;
                Navigation.ItemSearchResult();
            }
            else MessageBox.Show("There is no items to show", "Message!");
        }
    }
}
