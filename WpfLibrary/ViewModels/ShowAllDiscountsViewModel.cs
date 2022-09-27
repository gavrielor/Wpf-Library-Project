using ClassLibraryLibrary;
using ClassLibraryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class ShowAllDiscountsViewModel : ViewModelBase, IRefreshableViewModel
    {
        private Library library;
        private Discount selectedDiscount;

        public ICollection<Discount> AllDiscounts { get; set; }
        public Discount SelectedDiscount { get => selectedDiscount; set { Set(ref selectedDiscount, value); RemoveDiscountCommand.RaiseCanExecuteChanged(); } }

        public RelayCommand RemoveDiscountCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public ShowAllDiscountsViewModel(Library library)
        {
            this.library = library;

            AllDiscounts = library.GetAllDiscounts().ToList();

            RemoveDiscountCommand = new RelayCommand(RemoveDiscount, CanRemove);
            BackCommand = new RelayCommand(Navigation.Worker);
        }

        private bool CanRemove() => selectedDiscount != null;

        private void RemoveDiscount()
        {
            library.RemoveDiscount(SelectedDiscount);

            MessageBox.Show("Discount removed successfully", "Message");
            Navigation.Worker();
        }

        public void Refresh()
        {
            SelectedDiscount = null;
            AllDiscounts = library.GetAllDiscounts().ToList();
            RaisePropertyChanged(nameof(AllDiscounts));

            RemoveDiscountCommand.RaiseCanExecuteChanged();
        }
    }
}
