using ClassLibraryLibrary;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;

namespace WpfLibrary.ViewModels
{
    public class PropertySearchViewModel : ViewModelBase
    {
        private bool isEnabled = true;
        private string selectedProperty;
        private List<object> values;
        private object selectedValue;
        private Library library;

        public List<string> PropertiesList { get; set; }
        public string SelectedProperty
        {
            get => selectedProperty;
            set
            {
                Set(ref selectedProperty, value);
                Values = library.GetValuesByProperty(ViewModelLocator.ItemSearch.ItemType, value).ToList();
            }
        }
        public List<object> Values { get => values; set { Set(ref values, value); SelectedValue = value.FirstOrDefault(); } }
        public object SelectedValue { get => selectedValue; set { Set(ref selectedValue, value); } }

        public bool IsEnabled { get => isEnabled; set => Set(ref isEnabled, value); }

        public PropertySearchViewModel(Library library, List<string> propertiesList)
        {
            this.library = library;
            PropertiesList = propertiesList;
            SelectedProperty = PropertiesList.First();
        }
    }
}
