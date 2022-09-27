using ClassLibraryLibrary;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class RemoveItemViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        private Library library;


        [DataType(DataType.NotEmptyString)]
        public string ItemISBN { get; set; }

        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public RemoveItemViewModel(Library library)
        {
            this.library = library;

            RemoveCommand = new RelayCommand(Remove, () => !HasError);
            BackCommand = new RelayCommand(Navigation.Worker);
        }

        private void Remove()
        {
            var item = library.Items[ItemISBN];
            if (item != null)
            {
                library.Items.RemoveItem(item);
                MessageBox.Show("The item was removed successfully", "Message!");
            }
            else MessageBox.Show("An item with the given ISBN was not found", "Message!");
        }

        public void Refresh()
        {
            ItemISBN = string.Empty;
            RemoveCommand.RaiseCanExecuteChanged();
        }
    }
}
