using ClassLibraryLibrary;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class EditItemQuantityViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        private Library library;


        [DataType(DataType.NotEmptyString)]
        public string ItemISBN { get; set; }

        [DataType(DataType.NotNegativeInteger)]
        public string ItemQuantity { get; set; }

        public RelayCommand ApplyCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public EditItemQuantityViewModel(Library library)
        {
            this.library = library;

            ApplyCommand = new RelayCommand(Apply, () => !HasError);
            BackCommand = new RelayCommand(Navigation.Worker);
        }

        private void Apply()
        {
            var item = library.Items[ItemISBN];
            if (item != null)
            {
                library.Items.EditItemQuantity(item, uint.Parse(ItemQuantity));
                MessageBox.Show("The quantity was changed successfully", "Message!");
            }
            else MessageBox.Show("An item with the given ISBN was not found", "Message!");
        }

        public void Refresh()
        {
            ItemISBN = ItemQuantity = string.Empty;
            ApplyCommand.RaiseCanExecuteChanged();
        }
    }
}
