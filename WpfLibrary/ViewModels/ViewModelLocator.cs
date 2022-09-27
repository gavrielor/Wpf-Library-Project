using ClassLibraryLibrary;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace WpfLibrary.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<Library>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<WorkerMenuViewModel>();
            SimpleIoc.Default.Register<AddItemViewModel>();
            SimpleIoc.Default.Register<EditItemQuantityViewModel>();
            SimpleIoc.Default.Register<RemoveItemViewModel>();
            SimpleIoc.Default.Register<AbstractItemDetailsViewModel>();
            SimpleIoc.Default.Register<BookDetailsViewModel>();
            SimpleIoc.Default.Register<JournalDetailsViewModel>();
            SimpleIoc.Default.Register<ItemSearchViewModel>();
            SimpleIoc.Default.Register<ItemSearchResultViewModel>();
            SimpleIoc.Default.Register<AddDiscountViewModel>();
            SimpleIoc.Default.Register<ShowAllDiscountsViewModel>();
        }

        public static MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public static WelcomeViewModel Welcome => ServiceLocator.Current.GetInstance<WelcomeViewModel>();
        public static WorkerMenuViewModel Worker => ServiceLocator.Current.GetInstance<WorkerMenuViewModel>();
        public static AddItemViewModel AddItem => ServiceLocator.Current.GetInstance<AddItemViewModel>();
        public static EditItemQuantityViewModel EditItemQuantity => ServiceLocator.Current.GetInstance<EditItemQuantityViewModel>();
        public static RemoveItemViewModel RemoveItem => ServiceLocator.Current.GetInstance<RemoveItemViewModel>();
        public static AbstractItemDetailsViewModel AbstractItemDetails => ServiceLocator.Current.GetInstance<AbstractItemDetailsViewModel>();
        public static BookDetailsViewModel BookDetails => ServiceLocator.Current.GetInstance<BookDetailsViewModel>();
        public static JournalDetailsViewModel JournalDetails => ServiceLocator.Current.GetInstance<JournalDetailsViewModel>();
        public static ItemSearchViewModel ItemSearch => ServiceLocator.Current.GetInstance<ItemSearchViewModel>();
        public static ItemSearchResultViewModel ItemSearchResult => ServiceLocator.Current.GetInstance<ItemSearchResultViewModel>();
        public static AddDiscountViewModel AddDiscount => ServiceLocator.Current.GetInstance<AddDiscountViewModel>();
        public static ShowAllDiscountsViewModel ShowAllDiscounts => ServiceLocator.Current.GetInstance<ShowAllDiscountsViewModel>();
    }
}