using GalaSoft.MvvmLight;
using WpfLibrary.ViewModels;

namespace WpfLibrary.ViewModelsNavigation
{
    public class Navigation
    {
        public static void AddDiscount() => SetMain(ViewModelLocator.AddDiscount);
        public static void AddItem() => SetMain(ViewModelLocator.AddItem);
        public static void EditItemQuantity() => SetMain(ViewModelLocator.EditItemQuantity);
        public static void RemoveItem() => SetMain(ViewModelLocator.RemoveItem);
        public static void ItemSearch() => SetMain(ViewModelLocator.ItemSearch);
        public static void ItemSearchResult() => SetMain(ViewModelLocator.ItemSearchResult);
        public static void Welcome() => SetMain(ViewModelLocator.Welcome);
        public static void Worker() => SetMain(ViewModelLocator.Worker);
        public static void ShowAllDiscounts() => SetMain(ViewModelLocator.ShowAllDiscounts);

        private static void SetMain(ViewModelBase viewModel)
        {
            if (viewModel is IRefreshableViewModel refreshable) refreshable.Refresh();
            ViewModelLocator.Main.CurrentView = viewModel;
        }
    }
}
