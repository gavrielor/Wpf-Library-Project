using ClassLibraryLibrary;
using ClassLibraryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class ItemSearchViewModel : ViewModelBase, IRefreshableViewModel
    {
        private Library library;
        private List<string> allpropertiesList;
        private string itemISBN;
        private string itemName;

        public string ItemISBN { get => itemISBN; set { itemISBN = value; FindByISBNCommand.RaiseCanExecuteChanged(); } }
        public string ItemName { get => itemName; set { itemName = value; SearchByNameCommand.RaiseCanExecuteChanged(); } }

        public Type ItemType => ViewModelLocator.Welcome.SelectedType;
        public List<string> AvailableProperties => allpropertiesList.Except(ViewItemsList.Select(v => v.SelectedProperty)).ToList();

        public RelayCommand FindByISBNCommand { get; set; }
        public RelayCommand SearchByNameCommand { get; set; }
        public RelayCommand AddPropCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public ObservableCollection<PropertySearchViewModel> ViewItemsList { get; set; }
        public PropertySearchViewModel LastAdded => ViewItemsList.Last();

        public ItemSearchViewModel(Library library)
        {
            this.library = library;
            allpropertiesList = ItemType.GetProperties().Select(t => t.Name).Where(n => n != "Name" && n != "ISBN").ToList();

            FindByISBNCommand = new RelayCommand(FindByISBN, () => !string.IsNullOrEmpty(ItemISBN));
            SearchByNameCommand = new RelayCommand(SearchByName, () => !string.IsNullOrEmpty(ItemName));
            AddPropCommand = new RelayCommand(AddProperty, () => AvailableProperties.Any());
            SearchCommand = new RelayCommand(Search, () => ViewItemsList.Any());
            BackCommand = new RelayCommand(Navigation.Welcome);

            ViewItemsList = new ObservableCollection<PropertySearchViewModel>();
        }

        private void AddProperty()
        {
            if (ViewItemsList.Any()) LastAdded.IsEnabled = false;
            ViewItemsList.Add(new PropertySearchViewModel(library, AvailableProperties));

            AddPropCommand.RaiseCanExecuteChanged();
            SearchCommand.RaiseCanExecuteChanged();
        }

        private void Search()
        {
            List<(string, object)> filters = new List<(string, object)>();
            foreach (var filter in ViewItemsList)
            {
                filters.Add((filter.SelectedProperty, filter.SelectedValue));
            }

            var resultList = library.Items.Filter(ItemType, filters);
            if (resultList.Any())
            {
                ViewModelLocator.ItemSearchResult.ResultList = resultList;
                Navigation.ItemSearchResult();
            }
            else MessageBox.Show("There is no items with the given properties.", "Message!");
        }

        private void SearchByName()
        {
            var list = library.Items.SearchByName(ItemName).ToList();
            if (list.Any())
            {
                ViewModelLocator.ItemSearchResult.ResultList = list;
                Navigation.ItemSearchResult();
            }
            else MessageBox.Show("There is no item with the given name.", "Message!");
        }

        private void FindByISBN()
        {
            var item = library.Items[ItemISBN];
            if (item == null) MessageBox.Show("There is no item with the given ISBN.", "Message!");
            else
            {
                var list = new List<AbstractItem>();
                list.Add(item);
                ViewModelLocator.ItemSearchResult.ResultList = list;
                Navigation.ItemSearchResult();
            }
        }

        public void Refresh()
        {
            ItemISBN = ItemName = string.Empty;
            ViewItemsList.Clear();
            allpropertiesList = ItemType.GetProperties().Select(t => t.Name).Where(n => n != "Name" && n != "ISBN").ToList();
        }
    }
}
