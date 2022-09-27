using ClassLibraryLibrary;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class WorkerMenuViewModel : ViewModelBase
    {
        public IEnumerable<Type> AllTypes { get; set; }
        public Type SelectedType { get; set; }

        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand EditItemQuantityCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }
        public RelayCommand AddDiscountCommand { get; set; }
        public RelayCommand ShowAllDiscountsCommand { get; set; }
        public RelayCommand GoHomeCommand { get; set; }

        public WorkerMenuViewModel(Library library)
        {
            AllTypes = library.GetAllItemTypes();
            SelectedType = AllTypes.First();

            AddItemCommand = new RelayCommand(Navigation.AddItem);
            EditItemQuantityCommand = new RelayCommand(Navigation.EditItemQuantity);
            RemoveItemCommand = new RelayCommand(Navigation.RemoveItem);
            AddDiscountCommand = new RelayCommand(Navigation.AddDiscount);
            ShowAllDiscountsCommand = new RelayCommand(Navigation.ShowAllDiscounts);
            GoHomeCommand = new RelayCommand(Navigation.Welcome);
        }
    }
}
