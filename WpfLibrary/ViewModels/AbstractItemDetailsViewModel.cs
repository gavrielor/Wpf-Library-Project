using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class AbstractItemDetailsViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        [DataType(DataType.NotEmptyString)]
        public string Name { get; set; }

        [DataType(DataType.NotEmptyString)]
        public string ISBN { get; set; }

        [DataType(DataType.NotNegativeDouble)]
        public string Price { get; set; }

        [DataType(DataType.NotNegativeInteger)]
        public string Quantity { get; set; }

        public void Refresh()
        {
            Name = ISBN = Price = Quantity = string.Empty;
        }
    }
}
