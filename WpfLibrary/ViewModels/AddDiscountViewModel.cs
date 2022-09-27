using ClassLibraryLibrary;
using ClassLibraryLibrary.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class AddDiscountViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        private Library library;

        private Type selectedType;
        private IEnumerable<string> allProperties;
        private string selectedProperty;
        private IEnumerable<object> allValues;
        private object selectedValue;

        public IEnumerable<Type> AllTypes { get; set; }
        public Type SelectedType
        {
            get => selectedType;
            set
            {
                Set(ref selectedType, value);
                AllProperties = library.GetPropertiesByType(selectedType);
            }
        }

        public IEnumerable<string> AllProperties { get => allProperties; set { SelectedProperty = value.First(); Set(ref allProperties, value); } }
        public string SelectedProperty
        {
            get => selectedProperty;
            set
            {
                Set(ref selectedProperty, value);
                AllValues = library.GetValuesByProperty(selectedType, selectedProperty);
                SelectedValue = AllValues.FirstOrDefault();
            }
        }

        public IEnumerable<object> AllValues { get => allValues; set => Set(ref allValues, value); }
        public object SelectedValue { get => selectedValue; set { Set(ref selectedValue, value); AddDiscountCommand?.RaiseCanExecuteChanged(); } }


        [DataType(DataType.DiscountPercentages)]
        public string DiscountValue { get; set; }

        public RelayCommand AddDiscountCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public AddDiscountViewModel(Library library)
        {
            this.library = library;

            AllTypes = library.GetAllItemTypes();
            SelectedType = AllTypes.First();

            AddDiscountCommand = new RelayCommand(AddDiscount, () => ((selectedValue != null) && (!HasError)));
            BackCommand = new RelayCommand(Navigation.Worker);
        }

        private void AddDiscount()
        {
            library.AddDiscount(new Discount(SelectedType, double.Parse(DiscountValue), SelectedProperty, SelectedValue));
            MessageBox.Show("Discount added successfully!", "Message");
        }

        public void Refresh()
        {
            DiscountValue = string.Empty;
            AddDiscountCommand.RaiseCanExecuteChanged();
        }
    }
}
